using Microsoft.EntityFrameworkCore; 
using Portfolio.Data;
using Portfolio.Models;


public class ExperienceRepository : IExperienceRepository
{

private readonly PortfolioDbContext _context;

public ExperienceRepository(PortfolioDbContext context){
    _context = context;
}


public async Task<List<Experience>> GetAllAsync()
{
    return await _context.Experiences.OrderBy(e => e.Id).ToListAsync();
}



public async Task<Experience?> GetByIdAsync(int id)
{
    return await _context. Experiences.FirstOrDefaultAsync(e => e.Id == id);
}



//TODO Only Allow three At a Time
public async Task AddAsync(Experience experience)
{
        await _context.Experiences.AddAsync(experience);
        await _context.SaveChangesAsync ();
}



public async Task UpdateAsync(Experience updated)
{
    var existing = await _context. Experiences.FirstOrDefaultAsync(e => e.Id == updated.Id);

    if (existing == null)
    throw new Exception("Experience not found");

    existing.Period = updated.Period;
    existing.Role = updated.Role;
    existing.Company = updated.Company;
    existing.Description = updated.Description;
    existing. Tags = updated. Tags;
    existing. Responsibilities = updated. Responsibilities;

    await _context.SaveChangesAsync();
}



public async Task DeleteAsync(int id)
{
    var existing = await _context.Experiences.FirstOrDefaultAsync(e => e.Id == id);

    if (existing == null)
    throw new Exception("Experience not found");

    _context.Experiences.Remove(existing);
    await _context.SaveChangesAsync();
}



}