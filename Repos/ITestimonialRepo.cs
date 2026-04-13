using Portfolio.Models;

public interface ITestimonialRepository
{
    Task<List<Testimonial>> GetAllAsync();
    Task<Testimonial?> GetByIdAsync(int id);

    Task AddAsync(Testimonial testimonial);
    Task UpdateAsync(Testimonial testimonial);
    Task DeleteAsync(int id);
}