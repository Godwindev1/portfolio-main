namespace Portfolio.Models;
public class Testimonial
{
    public int Id { get; set; }
    public string Quote { get; set; } = string.Empty;
    public string AuthorInitials { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorTitle { get; set; } = string.Empty;
    public string TestimonialLink {get; set;} = string.Empty;
}
