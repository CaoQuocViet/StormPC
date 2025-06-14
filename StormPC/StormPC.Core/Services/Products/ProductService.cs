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
                LowestPrice = cheapestSpec?.Price ?? 0,
                CPU = cheapestSpec?.CPU ?? "Chưa có thông tin",
                GPU = cheapestSpec?.GPU ?? "Chưa có thông tin",
                RAM = cheapestSpec?.RAM ?? 0,
                Storage = cheapestSpec?.Storage ?? 0,
                StorageType = cheapestSpec?.StorageType ?? "Chưa có thông tin"
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
        // Lấy tất cả laptop với thông tin hiển thị
        var laptops = (await GetAllLaptopsForDisplayAsync()).ToList();

        // Lấy số lượng tùy chọn trong một truy vấn
        var optionsCounts = await _dbContext.LaptopSpecs
            .GroupBy(s => s.LaptopID)
            .Select(g => new { LaptopID = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.LaptopID, x => x.Count);

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

            // Lấy số lượng tùy chọn từ từ điển
            laptop.OptionsCount = optionsCounts.GetValueOrDefault(laptop.LaptopID, 0);
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
    
    public async Task<bool> CanEditLaptopAsync(int laptopId)
    {
        // Kiểm tra xem có specs nào của laptop đã từng được đặt hàng chưa
        var hasOrder = await _dbContext.Set<LaptopSpec>()
            .Where(s => s.LaptopID == laptopId)
            .AnyAsync(s => s.OrderItems.Any());
            
        return !hasOrder;
    }
    
    public async Task<bool> EditLaptopAsync(Laptop laptop)
    {
        try
        {
            // Tìm laptop cần sửa
            var existingLaptop = await _dbContext.Set<Laptop>()
                .FirstOrDefaultAsync(l => l.LaptopID == laptop.LaptopID);
                
            if (existingLaptop == null)
                return false;
                
            // Cập nhật thông tin
            existingLaptop.ModelName = laptop.ModelName;
            existingLaptop.BrandID = laptop.BrandID;
            existingLaptop.CategoryID = laptop.CategoryID;
            existingLaptop.ScreenSize = laptop.ScreenSize;
            existingLaptop.OperatingSystem = laptop.OperatingSystem;
            existingLaptop.ReleaseYear = laptop.ReleaseYear;
            existingLaptop.Discount = laptop.Discount;
            existingLaptop.Description = laptop.Description;
            existingLaptop.Picture = laptop.Picture;
            existingLaptop.UpdatedAt = DateTime.UtcNow;
            
            // Nếu có giảm giá, cập nhật thời gian giảm giá
            if (laptop.Discount > 0)
            {
                existingLaptop.DiscountStartDate = DateTime.UtcNow;
                existingLaptop.DiscountEndDate = DateTime.UtcNow.AddMonths(1);
            }
            else
            {
                existingLaptop.DiscountStartDate = null;
                existingLaptop.DiscountEndDate = null;
            }
            
            // Đánh dấu entity là đã thay đổi để EF Core cập nhật tất cả các trường
            _dbContext.Entry(existingLaptop).State = EntityState.Modified;
            
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
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

    public async Task<bool> AddLaptopSpecAsync(LaptopSpec spec)
    {
        try
        {
            // Đảm bảo giá trị hợp lệ
            spec.IsDeleted = false;
            
            // Tạo SKU nếu chưa có
            if (string.IsNullOrEmpty(spec.SKU))
            {
                spec.SKU = await GenerateSkuAsync(spec.LaptopID);
            }
            
            // Làm tròn giá trị tiền tệ xuống đơn vị 1000 VNĐ
            spec.ImportPrice = Math.Floor(spec.ImportPrice / 1000) * 1000;
            spec.Price = Math.Floor(spec.Price / 1000) * 1000;
            
            // Thêm thời gian
            spec.CreatedAt = DateTime.UtcNow;
            
            _dbContext.Set<LaptopSpec>().Add(spec);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    public async Task<string> GenerateSkuAsync(int laptopId)
    {
        try
        {
            // Lấy thông tin laptop
            var laptop = await _dbContext.Set<Laptop>()
                .Include(l => l.Brand)
                .FirstOrDefaultAsync(l => l.LaptopID == laptopId);
                
            if (laptop == null || laptop.Brand == null)
                throw new Exception("Không tìm thấy laptop");
                
            // Lấy số lượng specs hiện tại
            var specsCount = await _dbContext.Set<LaptopSpec>()
                .Where(s => s.LaptopID == laptopId)
                .CountAsync();
                
            // Tạo SKU theo format: BRAND-MODELNAME-VARIANT
            // Ví dụ: DELL-XPS13-001
            var brandCode = laptop.Brand.BrandName.ToUpper().Replace(" ", "");
            var modelCode = laptop.ModelName.ToUpper().Replace(" ", "");
            var variantNumber = (specsCount + 1).ToString("D3");
            
            return $"{brandCode}-{modelCode}-{variantNumber}";
        }
        catch (Exception)
        {
            // Nếu có lỗi, tạo SKU ngẫu nhiên
            return $"SKU-{DateTime.UtcNow.Ticks}";
        }
    }
}