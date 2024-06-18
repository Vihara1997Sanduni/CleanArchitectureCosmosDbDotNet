using Microsoft.Azure.Cosmos;
using CleanArchitectureCosmosDb.Domain.Interfaces;
using CleanArchitectureCosmosDb.Domain.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitectureCosmosDb.Infrastructure.Repositories
{
    public class ProgramRepository : IProgramRepository
    {
        private readonly Container _container;

        public ProgramRepository(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<IEnumerable<Program>> GetAllAsync()
        {
            var query = _container.GetItemQueryIterator<Program>();
            var results = new List<Program>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<Program> GetByIdAsync(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<Program>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException)
            {
                return null;
            }
        }

        public async Task AddAsync(Program program)
        {
            await _container.CreateItemAsync(program, new PartitionKey(program.Id));
        }

        public async Task UpdateAsync(Program program)
        {
            await _container.UpsertItemAsync(program, new PartitionKey(program.Id));
        }

        public async Task DeleteAsync(string id)
        {
            await _container.DeleteItemAsync<Program>(id, new PartitionKey(id));
        }
    }
}
