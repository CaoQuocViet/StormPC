using Microsoft.EntityFrameworkCore;
using StormPC.Core.Infrastructure.Database.Contexts;
using StormPC.Core.Models.Products;
using StormPC.Core.Models.Products.Dtos;

namespace StormPC.Core.Services.Products;

public class ProductService : IProductService
{
    private readonly StormPCDbContext _dbContext;

    public ProductService(StormPCDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<LaptopDisplayDto>> GetAllLaptopsForDisplayAsync()
    {
        var laptops = await _dbContext.Set<Laptop>()
            .Include(l => l.Brand)
            .Include(l => l.Category)
            .Include(l => l.Specs)
            .ToListAsync();

        var result = new List<LaptopDisplayDto>();

        foreach (var laptop in laptops)
        {
            var cheapestSpec = laptop.Specs
                .OrderBy(s => s.Price)
                .FirstOrDefault();

            if (cheapestSpec == null) continue;

            result.Add(new LaptopDisplayDto
            {
                LaptopID = laptop.LaptopID,
                ModelName = laptop.ModelName,
                BrandName = laptop.Brand.BrandName,
                CategoryName = laptop.Category.CategoryName,
                Picture = laptop.Picture,
                ScreenSize = laptop.ScreenSize,
                OperatingSystem = laptop.OperatingSystem,
                ReleaseYear = laptop.ReleaseYear,
                Discount = laptop.Discount,
                DiscountEndDate = laptop.DiscountEndDate,
                LowestPrice = cheapestSpec.Price,
                CPU = cheapestSpec.CPU,
                GPU = cheapestSpec.GPU,
                RAM = cheapestSpec.RAM,
                Storage = cheapestSpec.Storage,
                StorageType = cheapestSpec.StorageType
            });
        }

        return result;
    }

    public async Task<Laptop?> GetLaptopByIdAsync(string id)
    {
        return await _dbContext.Set<Laptop>()
            .Include(l => l.Brand)
            .Include(l => l.Category)
            .Include(l => l.Specs)
            .FirstOrDefaultAsync(l => l.LaptopID == id);
    }

    public async Task<LaptopSpec?> GetCheapestSpecForLaptopAsync(string laptopId)
    {
        return await _dbContext.Set<LaptopSpec>()
            .Where(ls => ls.LaptopID == laptopId)
            .OrderBy(ls => ls.Price)
            .FirstOrDefaultAsync();
    }
} 