using Portfolio.Models;

public interface ICaseStudyRepository
{
    Task<List<CaseStudy>> GetAllAsync(bool includeDetails = false);
    Task<CaseStudy?> GetByIdAsync(int id, bool includeDetails = true);

    Task AddAsync(CaseStudy caseStudy);
    Task UpdateAsync(CaseStudy caseStudy);
    Task DeleteAsync(int id);
}