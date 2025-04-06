using StormPC.Core.Models.Customers;
using StormPC.Core.Models.Orders;
using StormPC.Core.Models.Products;

namespace StormPC.Core.Models.System.Search;

public class SearchResult
{
    public string Type { get; set; } = string.Empty;  // Loại kết quả (Brand, Category, City, Customer, Laptop, Order, PaymentMethod)
    public int Id { get; set; }                       // ID của item
    public string DisplayName { get; set; } = string.Empty;  // Tên hiển thị chính
    public string Description { get; set; } = string.Empty;  // Mô tả bổ sung
    public Dictionary<string, string> AdditionalInfo { get; set; } = new();  // Thông tin thêm dạng key-value
}

public class SearchResults
{
    public int TotalCount { get; set; }
    public List<SearchResult> Items { get; set; } = new();
    public Dictionary<string, int> TypeCounts { get; set; } = new();  // Số lượng kết quả theo từng loại
} 