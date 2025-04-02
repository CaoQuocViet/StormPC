using StormPC.Core.Models.Products;
using StormPC.Core.Models.Products.Dtos;

namespace StormPC.Core.Services.Products;

public interface IProductService
{
    Task<List<LaptopDisplayDto>> GetLaptopsAsync();
    Task<IEnumerable<LaptopDisplayDto>> GetAllLaptopsForDisplayAsync();
    Task<Laptop?> GetLaptopByIdAsync(int id);
    Task<LaptopSpec?> GetCheapestSpecForLaptopAsync(int laptopId);
    Task<int> GetVariantsCountAsync(int laptopId);
    
    // Thêm các phương thức mới
    Task<bool> AddLaptopAsync(Laptop laptop);
    Task<bool> DeleteLaptopAsync(int laptopId);
    Task<bool> DeleteMultipleLaptopsAsync(List<int> laptopIds);
    Task<bool> CanDeleteLaptopAsync(int laptopId);
    Task<IEnumerable<Brand>> GetAllBrandsAsync();
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
} 