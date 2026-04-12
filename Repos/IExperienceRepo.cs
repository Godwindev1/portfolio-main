using Portfolio.Models;

public interface IExperienceRepository
{
    Task<List<Experience>> GetAllAsync();
    Task<Experience?> GetByIdAsync(int id);

    Task AddAsync(Experience caseStudy);
    Task UpdateAsync(Experience caseStudy);
    Task DeleteAsync(int id);
}