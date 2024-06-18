using CleanArchitectureCosmosDb.Domain.Interfaces;
using CleanArchitectureCosmosDb.Application.DTOs;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitectureCosmosDb.Application.Services
{
    public class ProgramService
    {
        private readonly IProgramRepository _repository;
        private readonly IMapper _mapper;

        public ProgramService(IProgramRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProgramDto>> GetAllAsync()
        {
            var programs = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProgramDto>>(programs);
        }

        public async Task<ProgramDto> GetByIdAsync(string id)
        {
            var program = await _repository.GetByIdAsync(id);
            return _mapper.Map<ProgramDto>(program);
        }

        public async Task AddAsync(ProgramDto programDto)
        {
            var program = _mapper.Map<Domain.Entity.Program>(programDto);
            await _repository.AddAsync(program);
        }

        public async Task UpdateAsync(ProgramDto programDto)
        {
            var program = _mapper.Map<Domain.Entity.Program>(programDto);
            await _repository.UpdateAsync(program);
        }

        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
