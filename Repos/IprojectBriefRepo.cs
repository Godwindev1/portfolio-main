public interface IProjectBriefRepository
{
    Task<List<ProjectBrief>> GetAllAsync();
    Task<ProjectBrief?> GetByIdAsync(long id);
    Task AddAsync(ProjectBrief projectBrief);
    Task UpdateAsync(ProjectBrief projectBrief);
    Task DeleteAsync(long id);
}