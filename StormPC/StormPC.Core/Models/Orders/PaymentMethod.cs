using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StormPC.Core.Models.Orders;

[Table("PaymentMethods")]
public class PaymentMethod
{
    [Key]
    public int PaymentMethodID { get; set; }

    [Required]
    [MaxLength(50)]
    public string MethodName { get; set; } = null!;

    // Navigation property
    public ICollection<Order> Orders { get; set; } = new List<Order>();
} 