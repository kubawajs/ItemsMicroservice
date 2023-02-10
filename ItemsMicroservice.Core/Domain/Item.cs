using System.ComponentModel.DataAnnotations;

namespace ItemsMicroservice.Core.Domain;
public sealed class Item
{
    [StringLength(12)]
    public string Code { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; }
    
    public string Notes { get; set; }
    public string Color { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set;}
}
