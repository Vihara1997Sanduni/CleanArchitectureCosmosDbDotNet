namespace CleanArchitectureCosmosDb.Domain.Entity;

    public class ApplicationForm
    {
       public string Id { get; set; }
        public PersonalInformation PersonalInfo { get; set; }
        public AdditionalInformation AdditionalInformation { get; set; }
    }
    

