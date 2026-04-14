using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio.Models;

public class Certification
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ? Id { get; set; }
    public string Icon { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Grade { get; set; } = string.Empty;
    public string Year { get; set; } = string.Empty;
    public string Provider { get; set; } = string.Empty;
    public string? BadgeUrl { get; set; } = string.Empty;
}
