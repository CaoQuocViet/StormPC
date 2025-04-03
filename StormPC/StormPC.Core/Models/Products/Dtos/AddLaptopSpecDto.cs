using System.ComponentModel.DataAnnotations;

namespace StormPC.Core.Models.Products.Dtos;

public class AddLaptopSpecDto
{
    public int LaptopID { get; set; }
    
    [Required(ErrorMessage = "Vui lòng nhập CPU")]
    public string CPU { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Vui lòng nhập GPU")]
    public string GPU { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Vui lòng nhập RAM")]
    [Range(1, int.MaxValue, ErrorMessage = "RAM phải lớn hơn 0")]
    public int RAM { get; set; }
    
    [Required(ErrorMessage = "Vui lòng nhập dung lượng ổ cứng")]
    [Range(1, int.MaxValue, ErrorMessage = "Dung lượng ổ cứng phải lớn hơn 0")]
    public int Storage { get; set; }
    
    [Required(ErrorMessage = "Vui lòng chọn loại ổ cứng")]
    public string StorageType { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Vui lòng chọn màu sắc")]
    public string Color { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Vui lòng nhập giá nhập")]
    [Range(1, double.MaxValue, ErrorMessage = "Giá nhập phải lớn hơn 0")]
    public decimal ImportPrice { get; set; }
    
    [Required(ErrorMessage = "Vui lòng nhập giá bán")]
    [Range(1, double.MaxValue, ErrorMessage = "Giá bán phải lớn hơn 0")]
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = "Vui lòng nhập số lượng tồn kho")]
    [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn kho không được âm")]
    public int StockQuantity { get; set; }
} 