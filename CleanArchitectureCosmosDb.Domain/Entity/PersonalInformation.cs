namespace CleanArchitectureCosmosDb.Domain.Entity;

  public class PersonalInformation
    {
       public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public string CurrentResidence { get; set; }
        public string IdNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public List<Question> Questions { get; set; }
    }

