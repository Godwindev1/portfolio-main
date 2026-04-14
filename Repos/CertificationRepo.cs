using Portfolio.Models;
using Microsoft.EntityFrameworkCore; 
using Portfolio.Data;



public class CertificationRepository : ICertificationRepository
{

private readonly PortfolioDbContext _context;

public CertificationRepository(PortfolioDbContext context){
    _context = context;
}


public async Task<List<Certification>> GetAllAsync()
{
    return await _context.Certifications.OrderBy(e => e.Id).ToListAsync();
}



public async Task<Certification?> GetByIdAsync(int id)
{
    return await _context.Certifications.FirstOrDefaultAsync(e => e.Id == id);
}



public async Task AddAsync(Certification certification)
{
        await _context.Certifications.AddAsync(certification);
        await _context.SaveChangesAsync ();
}



public async Task UpdateAsync(Certification updated)
{
    var existing = await _context. Certifications.FirstOrDefaultAsync(e => e.Id == updated.Id);

    if (existing == null)
    throw new Exception("Certifications not found");

    existing.BadgeUrl = updated.BadgeUrl;
    existing.Grade = updated.Grade;
    existing.Icon = updated.Icon;
    existing.Name = updated.Name;
    existing.Provider = updated.Provider;
    existing.Year = updated.Year;

    await _context.SaveChangesAsync();
}



public async Task DeleteAsync(int id)
{
    var existing = await _context.Certifications.FirstOrDefaultAsync(e => e.Id == id);

    if (existing == null)
    throw new Exception("Certifications not found");

    _context.Certifications.Remove(existing);
    await _context.SaveChangesAsync();
}



}