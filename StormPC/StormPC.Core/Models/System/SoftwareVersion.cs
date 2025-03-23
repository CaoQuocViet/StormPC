using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StormPC.Core.Models.System;

[Table("SoftwareVersion")]
public class SoftwareVersion
{
    [Key]
    public string Version { get; set; } = null!;
} 