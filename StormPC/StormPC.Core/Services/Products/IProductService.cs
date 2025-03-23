using StormPC.Core.Models.Products;
using StormPC.Core.Models.Products.Dtos;

namespace StormPC.Core.Services.Products;

public interface IProductService
{
    Task<IEnumerable<LaptopDisplayDto>> GetAllLaptopsForDisplayAsync();
    Task<Laptop?> GetLaptopByIdAsync(string id);
    Task<LaptopSpec?> GetCheapestSpecForLaptopAsync(string laptopId);
} 