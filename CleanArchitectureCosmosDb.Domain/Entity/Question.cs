namespace CleanArchitectureCosmosDb.Domain.Entity;

  public class Question
    {
        public string Id { get; set; }
        public string Type { get; set; } // Paragraph, YesNo, Dropdown, etc.
        public string Text { get; set; }
        public List<string> Options { get; set; } // For Dropdown and MultipleChoice
        public int? MaxChoices { get; set; } // For MultipleChoice
        public bool EnableOtherOption { get; set; } // For Dropdown
    }

