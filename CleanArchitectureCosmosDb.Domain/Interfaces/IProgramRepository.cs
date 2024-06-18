using CleanArchitectureCosmosDb.Domain.Entity;

namespace CleanArchitectureCosmosDb.Domain.Interfaces
{
    public interface IProgramRepository
    {
        Task<IEnumerable<Program>> GetAllAsync();
        Task<Program> GetByIdAsync(string id);
        Task AddAsync(Program program);
        Task UpdateAsync(Program program);
        Task DeleteAsync(string id);
    }
}
