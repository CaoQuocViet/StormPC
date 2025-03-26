using System;

namespace StormPC.Core.Models.Orders.Dtos;

public class OrderDisplayDto
{
    public int OrderID { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string StatusName { get; set; } = string.Empty;
    public string StatusColor { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string FormattedTotalAmount { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;
    public string ShippingCity { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public string FormattedOrderDate { get; set; } = string.Empty;
} 