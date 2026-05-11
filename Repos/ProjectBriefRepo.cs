using System.Data.Entity;
using Portfolio.Data;

public class ProjectBriefRepository : IProjectBriefRepository
{
    private readonly PortfolioDbContext _context;

    public ProjectBriefRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProjectBrief>> GetAllAsync()
    {
        return  _context.ProjectBriefs.ToList();
    }

    public async Task<ProjectBrief?> GetByIdAsync(long id)
    {
        return await _context.ProjectBriefs.FindAsync(id);
    }

    public async Task AddAsync(ProjectBrief projectBrief)
    {
        await _context.ProjectBriefs.AddAsync(projectBrief);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProjectBrief projectBrief)
    {
        _context.ProjectBriefs.Update(projectBrief);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var projectBrief = await _context.ProjectBriefs.FindAsync(id);
        if (projectBrief is not null)
        {
            _context.ProjectBriefs.Remove(projectBrief);
            await _context.SaveChangesAsync();
        }
    }
}