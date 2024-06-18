namespace CleanArchitectureCosmosDb.Domain.Entity;

    public class Program
    {
        public string Id { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public List<ApplicationForm> ApplicationForms { get; set; }
    }

