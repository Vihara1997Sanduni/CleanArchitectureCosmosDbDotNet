namespace CleanArchitectureCosmosDb.Application.DTOs;

public class ProgramDto
    {
        public string Id { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public List<ApplicationFormDto> ApplicationForms { get; set; }
    }

