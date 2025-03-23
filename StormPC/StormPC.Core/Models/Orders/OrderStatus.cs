using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StormPC.Core.Models.Orders;

[Table("OrderStatuses")]
public class OrderStatus
{
    [Key]
    public int StatusID { get; set; }

    [Required]
    [MaxLength(50)]
    public string StatusName { get; set; } = null!;

    // Navigation property
    public ICollection<Order> Orders { get; set; } = new List<Order>();
} 