using System.Threading.Tasks;
using CleanArchitectureCosmosDb.Domain.Entity;

namespace CleanArchitectureCosmosDb.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAndPasswordAsync(string username, string password);
    }
}
