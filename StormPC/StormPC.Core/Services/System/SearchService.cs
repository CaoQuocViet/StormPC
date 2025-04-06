using Microsoft.EntityFrameworkCore;
using StormPC.Core.Infrastructure.Database.Contexts;
using StormPC.Core.Models.System.Search;

namespace StormPC.Core.Services.System;

public class SearchService : ISearchService
{
    private readonly StormPCDbContext _context;

    public SearchService(StormPCDbContext context)
    {
        _context = context;
    }

    public async Task<SearchResults> SearchAsync(string query, int page = 1, int pageSize = 20)
    {
        var results = new SearchResults();
        var skip = (page - 1) * pageSize;
        query = query.ToLower().Trim();

        // Search Brands
        var brands = await _context.Brands
            .Where(b => !b.IsDeleted && b.BrandName.ToLower().Contains(query))
            .Skip(skip).Take(pageSize)
            .Select(b => new SearchResult
            {
                Type = "Brand",
                Id = b.BrandID,
                DisplayName = b.BrandName
            })
            .ToListAsync();
        results.Items.AddRange(brands);
        results.TypeCounts["Brand"] = await _context.Brands
            .Where(b => !b.IsDeleted && b.BrandName.ToLower().Contains(query))
            .CountAsync();

        // Search Categories
        var categories = await _context.Categories
            .Where(c => !c.IsDeleted && 
                (c.CategoryName.ToLower().Contains(query) || 
                (c.Description != null && c.Description.ToLower().Contains(query))))
            .Skip(skip).Take(pageSize)
            .Select(c => new SearchResult
            {
                Type = "Category",
                Id = c.CategoryID,
                DisplayName = c.CategoryName,
                Description = c.Description ?? string.Empty
            })
            .ToListAsync();
        results.Items.AddRange(categories);
        results.TypeCounts["Category"] = await _context.Categories
            .Where(c => !c.IsDeleted && 
                (c.CategoryName.ToLower().Contains(query) || 
                (c.Description != null && c.Description.ToLower().Contains(query))))
            .CountAsync();

        // Search Cities
        var cities = await _context.Cities
            .Where(c => c.CityName.ToLower().Contains(query))
            .Skip(skip).Take(pageSize)
            .Select(c => new SearchResult
            {
                Type = "City",
                Id = c.Id,
                DisplayName = c.CityName,
                AdditionalInfo = new Dictionary<string, string>
                {
                    { "CityCode", c.CityCode ?? string.Empty }
                }
            })
            .ToListAsync();
        results.Items.AddRange(cities);
        results.TypeCounts["City"] = await _context.Cities
            .Where(c => c.CityName.ToLower().Contains(query))
            .CountAsync();

        // Search Customers
        var customers = await _context.Customers
            .Include(c => c.City)
            .Where(c => !c.IsDeleted && 
                (c.FullName.ToLower().Contains(query) || 
                c.Email.ToLower().Contains(query) || 
                c.Address.ToLower().Contains(query)))
            .Skip(skip).Take(pageSize)
            .Select(c => new SearchResult
            {
                Type = "Customer",
                Id = c.CustomerID,
                DisplayName = c.FullName,
                Description = c.Email,
                AdditionalInfo = new Dictionary<string, string>
                {
                    { "Phone", c.Phone ?? string.Empty },
                    { "Address", c.Address ?? string.Empty },
                    { "City", c.City != null ? c.City.CityName : string.Empty }
                }
            })
            .ToListAsync();
        results.Items.AddRange(customers);
        results.TypeCounts["Customer"] = await _context.Customers
            .Where(c => !c.IsDeleted && 
                (c.FullName.ToLower().Contains(query) || 
                c.Email.ToLower().Contains(query) || 
                c.Address.ToLower().Contains(query)))
            .CountAsync();

        // Search Laptops
        var laptops = await _context.Laptops
            .Include(l => l.Brand)
            .Include(l => l.Category)
            .Where(l => !l.IsDeleted && 
                (l.ModelName.ToLower().Contains(query) || 
                (l.Description != null && l.Description.ToLower().Contains(query))))
            .Skip(skip).Take(pageSize)
            .Select(l => new SearchResult
            {
                Type = "Laptop",
                Id = l.LaptopID,
                DisplayName = l.ModelName,
                Description = l.Description ?? string.Empty,
                AdditionalInfo = new Dictionary<string, string>
                {
                    { "Brand", l.Brand != null ? l.Brand.BrandName : string.Empty },
                    { "Category", l.Category != null ? l.Category.CategoryName : string.Empty },
                    { "ScreenSize", l.ScreenSize.ToString() ?? string.Empty },
                    { "ReleaseYear", l.ReleaseYear.ToString() ?? string.Empty }
                }
            })
            .ToListAsync();
        results.Items.AddRange(laptops);
        results.TypeCounts["Laptop"] = await _context.Laptops
            .Where(l => !l.IsDeleted && 
                (l.ModelName.ToLower().Contains(query) || 
                (l.Description != null && l.Description.ToLower().Contains(query))))
            .CountAsync();

        // Search Orders
        var orders = await _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Status)
            .Include(o => o.PaymentMethod)
            .Include(o => o.ShipCity)
            .Where(o => !o.IsDeleted && 
                (o.Customer != null && o.Customer.FullName.ToLower().Contains(query)) ||
                (o.Status != null && o.Status.StatusName.ToLower().Contains(query)))
            .Skip(skip).Take(pageSize)
            .Select(o => new SearchResult
            {
                Type = "Order",
                Id = o.OrderID,
                DisplayName = $"Order #{o.OrderID}",
                Description = o.Customer != null ? o.Customer.FullName : string.Empty,
                AdditionalInfo = new Dictionary<string, string>
                {
                    { "Status", o.Status != null ? o.Status.StatusName : string.Empty },
                    { "TotalAmount", o.TotalAmount.ToString("N0") + " đ" },
                    { "OrderDate", o.OrderDate.ToString("dd/MM/yyyy HH:mm") },
                    { "PaymentMethod", o.PaymentMethod != null ? o.PaymentMethod.MethodName : string.Empty },
                    { "ShipCity", o.ShipCity != null ? o.ShipCity.CityName : string.Empty }
                }
            })
            .ToListAsync();
        results.Items.AddRange(orders);
        results.TypeCounts["Order"] = await _context.Orders
            .Where(o => !o.IsDeleted && 
                (o.Customer != null && o.Customer.FullName.ToLower().Contains(query)) ||
                (o.Status != null && o.Status.StatusName.ToLower().Contains(query)))
            .CountAsync();

        // Search PaymentMethods
        var paymentMethods = await _context.PaymentMethods
            .Where(p => p.MethodName.ToLower().Contains(query))
            .Skip(skip).Take(pageSize)
            .Select(p => new SearchResult
            {
                Type = "PaymentMethod",
                Id = p.PaymentMethodID,
                DisplayName = p.MethodName
            })
            .ToListAsync();
        results.Items.AddRange(paymentMethods);
        results.TypeCounts["PaymentMethod"] = await _context.PaymentMethods
            .Where(p => p.MethodName.ToLower().Contains(query))
            .CountAsync();

        results.TotalCount = results.Items.Count;
        return results;
    }

    public async Task<SearchResults> SearchByTypeAsync(string query, string type, int page = 1, int pageSize = 20)
    {
        var results = new SearchResults();
        var skip = (page - 1) * pageSize;
        query = query.ToLower().Trim();

        switch (type.ToLower())
        {
            case "brand":
                var brands = await _context.Brands
                    .Where(b => !b.IsDeleted && b.BrandName.ToLower().Contains(query))
                    .Skip(skip).Take(pageSize)
                    .Select(b => new SearchResult
                    {
                        Type = "Brand",
                        Id = b.BrandID,
                        DisplayName = b.BrandName
                    })
                    .ToListAsync();
                results.Items.AddRange(brands);
                results.TotalCount = await _context.Brands
                    .Where(b => !b.IsDeleted && b.BrandName.ToLower().Contains(query))
                    .CountAsync();
                break;

            case "category":
                var categories = await _context.Categories
                    .Where(c => !c.IsDeleted && 
                        (c.CategoryName.ToLower().Contains(query) || 
                        (c.Description != null && c.Description.ToLower().Contains(query))))
                    .Skip(skip).Take(pageSize)
                    .Select(c => new SearchResult
                    {
                        Type = "Category",
                        Id = c.CategoryID,
                        DisplayName = c.CategoryName,
                        Description = c.Description ?? string.Empty
                    })
                    .ToListAsync();
                results.Items.AddRange(categories);
                results.TotalCount = await _context.Categories
                    .Where(c => !c.IsDeleted && 
                        (c.CategoryName.ToLower().Contains(query) || 
                        (c.Description != null && c.Description.ToLower().Contains(query))))
                    .CountAsync();
                break;

            case "city":
                var cities = await _context.Cities
                    .Where(c => c.CityName.ToLower().Contains(query))
                    .Skip(skip).Take(pageSize)
                    .Select(c => new SearchResult
                    {
                        Type = "City",
                        Id = c.Id,
                        DisplayName = c.CityName,
                        AdditionalInfo = new Dictionary<string, string>
                        {
                            { "CityCode", c.CityCode ?? string.Empty }
                        }
                    })
                    .ToListAsync();
                results.Items.AddRange(cities);
                results.TotalCount = await _context.Cities
                    .Where(c => c.CityName.ToLower().Contains(query))
                    .CountAsync();
                break;

            case "customer":
                var customers = await _context.Customers
                    .Include(c => c.City)
                    .Where(c => !c.IsDeleted && 
                        (c.FullName.ToLower().Contains(query) || 
                        c.Email.ToLower().Contains(query) || 
                        c.Address.ToLower().Contains(query)))
                    .Skip(skip).Take(pageSize)
                    .Select(c => new SearchResult
                    {
                        Type = "Customer",
                        Id = c.CustomerID,
                        DisplayName = c.FullName,
                        Description = c.Email,
                        AdditionalInfo = new Dictionary<string, string>
                        {
                            { "Phone", c.Phone ?? string.Empty },
                            { "Address", c.Address ?? string.Empty },
                            { "City", c.City != null ? c.City.CityName : string.Empty }
                        }
                    })
                    .ToListAsync();
                results.Items.AddRange(customers);
                results.TotalCount = await _context.Customers
                    .Where(c => !c.IsDeleted && 
                        (c.FullName.ToLower().Contains(query) || 
                        c.Email.ToLower().Contains(query) || 
                        c.Address.ToLower().Contains(query)))
                    .CountAsync();
                break;

            case "laptop":
                var laptops = await _context.Laptops
                    .Include(l => l.Brand)
                    .Include(l => l.Category)
                    .Where(l => !l.IsDeleted && 
                        (l.ModelName.ToLower().Contains(query) || 
                        (l.Description != null && l.Description.ToLower().Contains(query))))
                    .Skip(skip).Take(pageSize)
                    .Select(l => new SearchResult
                    {
                        Type = "Laptop",
                        Id = l.LaptopID,
                        DisplayName = l.ModelName,
                        Description = l.Description ?? string.Empty,
                        AdditionalInfo = new Dictionary<string, string>
                        {
                            { "Brand", l.Brand != null ? l.Brand.BrandName : string.Empty },
                            { "Category", l.Category != null ? l.Category.CategoryName : string.Empty },
                            { "ScreenSize", l.ScreenSize.ToString() ?? string.Empty },
                            { "ReleaseYear", l.ReleaseYear.ToString() ?? string.Empty }
                        }
                    })
                    .ToListAsync();
                results.Items.AddRange(laptops);
                results.TotalCount = await _context.Laptops
                    .Where(l => !l.IsDeleted && 
                        (l.ModelName.ToLower().Contains(query) || 
                        (l.Description != null && l.Description.ToLower().Contains(query))))
                    .CountAsync();
                break;

            case "order":
                var orders = await _context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.Status)
                    .Include(o => o.PaymentMethod)
                    .Include(o => o.ShipCity)
                    .Where(o => !o.IsDeleted && 
                        (o.Customer != null && o.Customer.FullName.ToLower().Contains(query)) ||
                        (o.Status != null && o.Status.StatusName.ToLower().Contains(query)))
                    .Skip(skip).Take(pageSize)
                    .Select(o => new SearchResult
                    {
                        Type = "Order",
                        Id = o.OrderID,
                        DisplayName = $"Order #{o.OrderID}",
                        Description = o.Customer != null ? o.Customer.FullName : string.Empty,
                        AdditionalInfo = new Dictionary<string, string>
                        {
                            { "Status", o.Status != null ? o.Status.StatusName : string.Empty },
                            { "TotalAmount", o.TotalAmount.ToString("N0") + " đ" },
                            { "OrderDate", o.OrderDate.ToString("dd/MM/yyyy HH:mm") },
                            { "PaymentMethod", o.PaymentMethod != null ? o.PaymentMethod.MethodName : string.Empty },
                            { "ShipCity", o.ShipCity != null ? o.ShipCity.CityName : string.Empty }
                        }
                    })
                    .ToListAsync();
                results.Items.AddRange(orders);
                results.TotalCount = await _context.Orders
                    .Where(o => !o.IsDeleted && 
                        (o.Customer != null && o.Customer.FullName.ToLower().Contains(query)) ||
                        (o.Status != null && o.Status.StatusName.ToLower().Contains(query)))
                    .CountAsync();
                break;

            case "paymentmethod":
                var paymentMethods = await _context.PaymentMethods
                    .Where(p => p.MethodName.ToLower().Contains(query))
                    .Skip(skip).Take(pageSize)
                    .Select(p => new SearchResult
                    {
                        Type = "PaymentMethod",
                        Id = p.PaymentMethodID,
                        DisplayName = p.MethodName
                    })
                    .ToListAsync();
                results.Items.AddRange(paymentMethods);
                results.TotalCount = await _context.PaymentMethods
                    .Where(p => p.MethodName.ToLower().Contains(query))
                    .CountAsync();
                break;
        }

        return results;
    }
} 