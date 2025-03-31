using System;
using System.Collections.Generic;

namespace StormPC.Core.Models.Orders.Dtos;

public class OrderDetailDto
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
    public List<OrderItemDto> Items { get; set; } = new();
}

public class OrderItemDto
{
    public int OrderItemID { get; set; }
    public int VariantID { get; set; }
    public string ModelName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string FormattedUnitPrice { get; set; } = string.Empty;
    public decimal Subtotal { get; set; }
    public string FormattedSubtotal { get; set; } = string.Empty;
} 