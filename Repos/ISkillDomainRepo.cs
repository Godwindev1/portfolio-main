using Portfolio.Models;

public interface ISkillDomainReposirtory
{
    Task<List<SkillDomain>> GetAllAsync();
    Task<SkillDomain?> GetByIdAsync(int id);

    Task AddAsync(SkillDomain skillDomain);
    Task UpdateAsync(SkillDomain skillDomain);
    Task DeleteAsync(int id);
}