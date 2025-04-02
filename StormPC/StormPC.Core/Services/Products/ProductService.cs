using Microsoft.EntityFrameworkCore;
using StormPC.Core.Infrastructure.Database.Contexts;
using StormPC.Core.Models.Products;
using StormPC.Core.Models.Products.Dtos;

namespace StormPC.Core.Services.Products;

public class ProductService(StormPCDbContext dbContext) : IProductService
{
    private readonly StormPCDbContext _dbContext = dbContext;

    public async Task<IEnumerable<LaptopDisplayDto>> GetAllLaptopsForDisplayAsync()
    {
        var laptops = await _dbContext.Set<Laptop>()
            .Where(l => !l.IsDeleted)
            .Include(l => l.Brand)
            .Include(l => l.Category)
            .Include(l => l.Specs)
            .ToListAsync();

        var result = new List<LaptopDisplayDto>();

        foreach (var laptop in laptops)
        {
            if (laptop.Brand == null || laptop.Category == null)
            {
                continue;
            }

            var cheapestSpec = laptop.Specs
                .Where(s => s != null)
                .OrderBy(s => s.Price)
                .FirstOrDefault();

            if (cheapestSpec == null)
            {
                continue;
            }

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
                CPU = cheapestSpec.CPU ?? "N/A",
                GPU = cheapestSpec.GPU ?? "N/A",
                RAM = cheapestSpec.RAM,
                Storage = cheapestSpec.Storage,
                StorageType = cheapestSpec.StorageType ?? "N/A"
            });
        }

        return result;
    }

    public async Task<Laptop?> GetLaptopByIdAsync(int id)
    {
        return await _dbContext.Set<Laptop>()
            .Where(l => !l.IsDeleted)
            .Include(l => l.Brand)
            .Include(l => l.Category)
            .Include(l => l.Specs)
            .FirstOrDefaultAsync(l => l.LaptopID == id);
    }

    public async Task<LaptopSpec?> GetCheapestSpecForLaptopAsync(int laptopId)
    {
        return await _dbContext.Set<LaptopSpec>()
            .Where(ls => ls.LaptopID == laptopId)
            .OrderBy(ls => ls.Price)
            .FirstOrDefaultAsync();
    }

    public async Task<int> GetVariantsCountAsync(int laptopId)
    {
        return await _dbContext.LaptopSpecs
            .Where(spec => spec.LaptopID == laptopId)
            .CountAsync();
    }

    public async Task<List<LaptopDisplayDto>> GetLaptopsAsync()
    {
        var laptops = (await GetAllLaptopsForDisplayAsync()).ToList();

        // Định dạng giá và tính phần trăm giảm giá
        foreach (var laptop in laptops)
        {
            laptop.FormattedPrice = string.Format("{0:N0}", laptop.LowestPrice);

            if (laptop.Discount > 0)
            {
                var originalPrice = laptop.LowestPrice + laptop.Discount;
                var discountPercent = (int)((laptop.Discount / originalPrice) * 100);
                laptop.FormattedDiscount = discountPercent.ToString();
            }
            else
            {
                laptop.FormattedDiscount = "0";
            }

            // Lấy số lượng phiên bản
            laptop.OptionsCount = await _dbContext.LaptopSpecs
                .CountAsync(s => s.LaptopID == laptop.LaptopID) - 1;
        }

        return laptops.OrderByDescending(l => l.ReleaseYear).ToList();
    }

    public async Task<bool> AddLaptopAsync(Laptop laptop)
    {
        try
        {
            // Đảm bảo giá trị hợp lệ
            laptop.IsDeleted = false;
            if (laptop.Discount < 0)
                laptop.Discount = 0;
                
            // Làm tròn giá trị tiền tệ xuống đơn vị 1000 VNĐ
            laptop.Discount = Math.Floor(laptop.Discount / 1000) * 1000;
            
            _dbContext.Set<Laptop>().Add(laptop);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    public async Task<bool> DeleteLaptopAsync(int laptopId)
    {
        try
        {
            // Kiểm tra xem có thể xóa laptop không
            if (!await CanDeleteLaptopAsync(laptopId))
                return false;
                
            // Tìm laptop để xóa
            var laptop = await _dbContext.Set<Laptop>()
                .Include(l => l.Specs)
                .FirstOrDefaultAsync(l => l.LaptopID == laptopId);
                
            if (laptop == null)
                return false;
                
            // Đánh dấu laptop là đã xóa
            laptop.IsDeleted = true;
            
            // Đánh dấu tất cả các specs là đã xóa
            foreach (var spec in laptop.Specs)
            {
                spec.IsDeleted = true;
            }
            
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    public async Task<bool> DeleteMultipleLaptopsAsync(List<int> laptopIds)
    {
        try
        {
            // Lọc ra danh sách laptop có thể xóa
            var deletableIds = new List<int>();
            foreach (var id in laptopIds)
            {
                if (await CanDeleteLaptopAsync(id))
                    deletableIds.Add(id);
            }
            
            if (deletableIds.Count == 0)
                return false;
                
            // Tìm các laptop để xóa
            var laptops = await _dbContext.Set<Laptop>()
                .Include(l => l.Specs)
                .Where(l => deletableIds.Contains(l.LaptopID))
                .ToListAsync();
                
            // Đánh dấu laptops và specs là đã xóa
            foreach (var laptop in laptops)
            {
                laptop.IsDeleted = true;
                foreach (var spec in laptop.Specs)
                {
                    spec.IsDeleted = true;
                }
            }
            
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    public async Task<bool> CanDeleteLaptopAsync(int laptopId)
    {
        // Kiểm tra xem có specs nào của laptop đã từng được đặt hàng chưa
        var hasOrder = await _dbContext.Set<LaptopSpec>()
            .Where(s => s.LaptopID == laptopId)
            .AnyAsync(s => s.OrderItems.Any());
            
        return !hasOrder;
    }
    
    public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
    {
        return await _dbContext.Set<Brand>()
            .Where(b => !b.IsDeleted)
            .OrderBy(b => b.BrandName)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _dbContext.Set<Category>()
            .Where(c => !c.IsDeleted)
            .OrderBy(c => c.CategoryName)
            .ToListAsync();
    }
} 