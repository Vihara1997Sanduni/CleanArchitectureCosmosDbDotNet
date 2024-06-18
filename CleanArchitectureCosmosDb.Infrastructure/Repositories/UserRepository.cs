using System.Linq;
using System.Threading.Tasks;
using CleanArchitectureCosmosDb.Domain.Entity; // Assuming CleanArchitectureCosmosDb.Domain.Entity.User is the correct type
using CleanArchitectureCosmosDb.Domain.Interfaces;
using Microsoft.Azure.Cosmos;

namespace CleanArchitectureCosmosDb.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Container _container;

        public UserRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<Domain.Entity.User> GetUserByUsernameAndPasswordAsync(string username, string password)
        {
            var queryText = "SELECT * FROM c WHERE c.username = @username AND c.password = @password";
            var queryDefinition = new QueryDefinition(queryText)
                .WithParameter("@username", username)
                .WithParameter("@password", password);

            var resultSetIterator = _container.GetItemQueryIterator<Domain.Entity.User>(queryDefinition); 

            while (resultSetIterator.HasMoreResults)
            {
                var response = await resultSetIterator.ReadNextAsync();
                return response.FirstOrDefault(); 
            }

            return null;
        }
    }
}
