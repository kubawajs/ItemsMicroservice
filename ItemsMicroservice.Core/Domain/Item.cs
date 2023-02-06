using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ItemsMicroservice.Core.Domain;
public sealed class Item
{
    [Key]
    [MaxLength(12)]
    public string Code { get; set; }

    [Required]
    // TODO: unique - [Index(nameof(Name), IsUnique = true)]
    [MaxLength(200)]
    public string Name { get; set; }
    
    public string Notes { get; set; }
    public string Color { get; set; }
}
