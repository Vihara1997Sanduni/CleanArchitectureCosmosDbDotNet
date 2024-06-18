namespace CleanArchitectureCosmosDb.Application.DTOs;

public class ApplicationFormDto
    {
        public string Id { get; set; }
        public PersonalInformationDto PersonalInfo { get; set; }
        public AdditionalInformationDto AdditionalInformation { get; set; }
    }

