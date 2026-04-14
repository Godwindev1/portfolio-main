using Portfolio.Models;
using Microsoft.EntityFrameworkCore; 
using Portfolio.Data;



public class SkillDomainRepository : ISkillDomainReposirtory
{

private readonly PortfolioDbContext _context;

public SkillDomainRepository(PortfolioDbContext context){
    _context = context;
}


public async Task<List<SkillDomain>> GetAllAsync()
{
    return await _context.SkillDomains.OrderBy(e => e.Id).ToListAsync();
}



public async Task<SkillDomain?> GetByIdAsync(int id)
{
    return await _context.SkillDomains.FirstOrDefaultAsync(e => e.Id == id);
}



public async Task AddAsync(SkillDomain domain)
{
        await _context.SkillDomains.AddAsync(domain);
        await _context.SaveChangesAsync ();
}



public async Task UpdateAsync(SkillDomain updated)
{
    var existing = await _context.SkillDomains.FirstOrDefaultAsync(e => e.Id == updated.Id);

    if (existing == null)
    throw new Exception("SkillDomains not found");

    existing.Icon = updated.Icon;
    existing.Items = updated.Items;
    existing.Number = updated.Number;
    existing.Number = updated.Number;
    existing.Title = updated.Title;

    await _context.SaveChangesAsync();
}



public async Task DeleteAsync(int id)
{
    var existing = await _context.SkillDomains.FirstOrDefaultAsync(e => e.Id == id);

    if (existing == null)
    throw new Exception("SkillDomains not found");

    _context.SkillDomains.Remove(existing);
    await _context.SaveChangesAsync();
}



}