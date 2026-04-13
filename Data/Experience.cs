
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Portfolio.Models;

[PrimaryKey("Id")]
public class Experience
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set; }
    public string Period { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = [];

    public List<string> Responsibilities { get; set; } = [];
}

