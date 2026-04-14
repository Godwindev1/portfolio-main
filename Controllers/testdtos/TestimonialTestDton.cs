using Portfolio.Models;

public static class TestimonialReturnDto 
{
    public static List<Testimonial> Get() 
    {
        return new List<Testimonial>
        {
            new() {
                Id = 1,
                Quote = "His ability to design systems that don't just work today but hold up under load tomorrow is rare. The job scheduler alone saved us weeks of integration pain.",
                AuthorInitials = "MK",
                AuthorName = "CLIENT // BACKEND_PROJECT",
                AuthorTitle = "VIA FIVERR // VERIFIED"
            },
            new() {
                Id = 2,
                Quote = "Documentation as disciplined as the code itself. Every architectural decision was explained. We didn't just get a system — we got a foundation we understand.",
                AuthorInitials = "AO",
                AuthorName = "CLIENT // RAG_PIPELINE",
                AuthorTitle = "ASP.NET CORE PROJECT // VERIFIED"
            }
        };
    }
} 
