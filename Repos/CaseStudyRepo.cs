using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models;

public class CaseStudyRepository : ICaseStudyRepository
{
    private readonly PortfolioDbContext _context;

    public CaseStudyRepository(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<List<CaseStudy>> GetAllAsync(bool includeDetails = false)
    {
        var query = _context.CaseStudies.AsQueryable();

        if (includeDetails)
            query = IncludeAll(query);

        return await query
            .OrderBy(c => c.DisplayOrder)
            .ToListAsync();
    }

    public async Task<CaseStudy?> GetByIdAsync(int id, bool includeDetails = true)
    {
        var query = _context.CaseStudies.AsQueryable();

        if (includeDetails)
            query = IncludeAll(query);

        return await query.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(CaseStudy caseStudy)
    {
        await _context.CaseStudies.AddAsync(caseStudy);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CaseStudy updated)
    {
        var existing = await _context.CaseStudies
            .IncludeAllChildren()
            .FirstOrDefaultAsync(c => c.Id == updated.Id);

        if (existing == null)
            throw new Exception("CaseStudy not found");

        // ---- Update scalar fields ----
        existing.Title = updated.Title;
        existing.Label = updated.Label;
        existing.Category = updated.Category;
        existing.Summary = updated.Summary;
        existing.CoverImageUrl = updated.CoverImageUrl;
        existing.DisplayOrder = updated.DisplayOrder;
        existing.IsFeatured = updated.IsFeatured;
        existing.ProblemJson = updated.ProblemJson;
        existing.SolutionJson = updated.SolutionJson;

        // ---- Sync child collections ----
        SyncCollection(existing.ImplementationSteps, updated.ImplementationSteps);
        SyncCollection(existing.Metrics, updated.Metrics);
        SyncCollection(existing.Skills, updated.Skills);
        SyncCollection(existing.Artifacts, updated.Artifacts);
        SyncCollection(existing.ArchitectureComponents, updated.ArchitectureComponents);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.CaseStudies.FindAsync(id);
        if (entity == null) return;

        _context.CaseStudies.Remove(entity);
        await _context.SaveChangesAsync();
    }

    // -------------------------
    // Helpers
    // -------------------------

    private static IQueryable<CaseStudy> IncludeAll(IQueryable<CaseStudy> query)
    {
        return query
            .Include(c => c.ImplementationSteps)
            .Include(c => c.Metrics)
            .Include(c => c.Skills)
            .Include(c => c.Artifacts)
            .Include(c => c.ArchitectureComponents);
    }

    private void SyncCollection<T>(ICollection<T> existing, ICollection<T> updated)
        where T : class
    {
        // Remove deleted
        var toRemove = existing.Where(e => !updated.Any(u => Match(e, u))).ToList();
        foreach (var item in toRemove)
            existing.Remove(item);

        // Add or update
        foreach (var updatedItem in updated)
        {
            var existingItem = existing.FirstOrDefault(e => Match(e, updatedItem));

            if (existingItem == null)
            {
                existing.Add(updatedItem);
            }
            else
            {
                _context.Entry(existingItem).CurrentValues.SetValues(updatedItem);
            }
        }
    }

    private bool Match<T>(T a, T b)
    {
        var prop = typeof(T).GetProperty("Id");
        if (prop == null) return false;

        var aId = (int)prop.GetValue(a)!;
        var bId = (int)prop.GetValue(b)!;

        return aId != 0 && aId == bId;
    }
}



public static class CaseStudyQueryExtensions
{
    public static IQueryable<CaseStudy> IncludeAllChildren(this IQueryable<CaseStudy> query)
    {
        return query
            .Include(c => c.ImplementationSteps)
            .Include(c => c.Metrics)
            .Include(c => c.Skills)
            .Include(c => c.Artifacts)
            .Include(c => c.ArchitectureComponents);
    }
}