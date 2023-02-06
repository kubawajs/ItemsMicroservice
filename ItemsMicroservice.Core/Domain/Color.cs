using System.ComponentModel.DataAnnotations;

namespace ItemsMicroservice.Core.Domain;
public sealed class Color
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}
