using AutoMapper;
using CleanArchitectureCosmosDb.Domain.Entity;
using CleanArchitectureCosmosDb.Application.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Program, ProgramDto>();
        CreateMap<ProgramDto, Program>();

        CreateMap<Question, QuestionDto>();
        CreateMap<QuestionDto, Question>();

        CreateMap<PersonalInformation, PersonalInformationDto>();
        CreateMap<PersonalInformationDto, PersonalInformation>();

        CreateMap<ApplicationForm, ApplicationFormDto>();
        CreateMap<ApplicationFormDto, ApplicationForm>();

        CreateMap<AdditionalInformation, AdditionalInformationDto>();
        CreateMap<AdditionalInformationDto, AdditionalInformation>();

        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();

    }
}
