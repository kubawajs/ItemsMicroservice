using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ItemsMicroservice.Core.Domain;
public sealed class Item
{
    [MaxLength(12)]
    public string Code { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; }
    
    public string Notes { get; set; }
    public string Color { get; set; }
}
