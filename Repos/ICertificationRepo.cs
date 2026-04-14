using Portfolio.Models;

public interface ICertificationRepository
{
    Task<List<Certification>> GetAllAsync();
    Task<Certification?> GetByIdAsync(int id);

    Task AddAsync(Certification certification);
    Task UpdateAsync(Certification certification);
    Task DeleteAsync(int id);
}