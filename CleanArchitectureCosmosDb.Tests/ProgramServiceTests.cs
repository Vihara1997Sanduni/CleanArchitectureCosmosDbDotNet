using AutoMapper;
using CleanArchitectureCosmosDb.Application.DTOs;
using CleanArchitectureCosmosDb.Application.Services;
using CleanArchitectureCosmosDb.Domain.Entity;
using CleanArchitectureCosmosDb.Domain.Interfaces;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitectureCosmosDb.Tests
{
    public class ProgramServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IProgramRepository> _programRepositoryMock;
        private readonly ProgramService _programService;

        public ProgramServiceTests()
        {
            // Setup AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Program, ProgramDto>().ReverseMap();
                cfg.CreateMap<ApplicationForm, ApplicationFormDto>().ReverseMap();
                cfg.CreateMap<PersonalInformation, PersonalInformationDto>().ReverseMap();
                cfg.CreateMap<AdditionalInformation, AdditionalInformationDto>().ReverseMap();
                cfg.CreateMap<Question, QuestionDto>().ReverseMap();
            });

            _mapper = config.CreateMapper();

            // Setup Mock Repository
            _programRepositoryMock = new Mock<IProgramRepository>();

            // Create ProgramService instance
            _programService = new ProgramService(_programRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllPrograms()
        {
            // Arrange
            var programs = new List<Program>
            {
                new Program { Id = "1", Topic = "Topic1", Description = "Description1" },
                new Program { Id = "2", Topic = "Topic2", Description = "Description2" }
            };
            _programRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(programs);

            // Act
            var result = await _programService.GetAllAsync();

            // Assert
            var resultList = result.ToList();
            Assert.Equal(2, resultList.Count);
            Assert.Equal("1", resultList[0].Id);
            Assert.Equal("Topic1", resultList[0].Topic);
            Assert.Equal("Description1", resultList[0].Description);
            Assert.Equal("2", resultList[1].Id);
            Assert.Equal("Topic2", resultList[1].Topic);
            Assert.Equal("Description2", resultList[1].Description);
        }
    }
}
