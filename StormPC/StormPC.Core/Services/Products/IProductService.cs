using StormPC.Core.Models.Products;
using StormPC.Core.Models.Products.Dtos;

namespace StormPC.Core.Services.Products;

public interface IProductService
{
    Task<List<LaptopDisplayDto>> GetLaptopsAsync();
    Task<IEnumerable<LaptopDisplayDto>> GetAllLaptopsForDisplayAsync();
    Task<Laptop?> GetLaptopByIdAsync(string id);
    Task<LaptopSpec?> GetCheapestSpecForLaptopAsync(string laptopId);
    Task<int> GetVariantsCountAsync(string laptopId);
} 