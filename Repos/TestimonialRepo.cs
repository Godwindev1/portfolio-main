using Portfolio.Models;
using Microsoft.EntityFrameworkCore; 
using Portfolio.Data;



public class TestimonialRepository : ITestimonialRepository
{

private readonly PortfolioDbContext _context;

public TestimonialRepository(PortfolioDbContext context){
    _context = context;
}


public async Task<List<Testimonial>> GetAllAsync()
{
    return await _context.Testimonials.OrderBy(e => e.Id).ToListAsync();
}



public async Task<Testimonial?> GetByIdAsync(int id)
{
    return await _context.Testimonials.FirstOrDefaultAsync(e => e.Id == id);
}



public async Task AddAsync(Testimonial testimonial)
{
        await _context.Testimonials.AddAsync(testimonial);
        await _context.SaveChangesAsync ();
}



public async Task UpdateAsync(Testimonial updated)
{
    var existing = await _context. Testimonials.FirstOrDefaultAsync(e => e.Id == updated.Id);

    if (existing == null)
    throw new Exception("Testimonial not found");

    existing.AuthorInitials = updated.AuthorInitials;
    existing.AuthorName = updated.AuthorName;
    existing.AuthorTitle = updated.AuthorTitle;
    existing.Quote = updated.Quote;
    existing.TestimonialLink = updated.TestimonialLink;



    await _context.SaveChangesAsync();
}



public async Task DeleteAsync(int id)
{
    var existing = await _context.Testimonials.FirstOrDefaultAsync(e => e.Id == id);

    if (existing == null)
    throw new Exception("Testimonial not found");

    _context.Testimonials.Remove(existing);
    await _context.SaveChangesAsync();
}



}