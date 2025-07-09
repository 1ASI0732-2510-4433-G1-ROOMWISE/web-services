using SweetManagerWebService.ResourceManagement.Domain.Model.Aggregates;

namespace SweetManagerWebService.ResourceManagement.Domain.Model.Entities
{
    // Represents a type of report with associated properties
    public partial class TypeReport
    {
        // Unique identifier for the type of report
        public int Id { get; }
        
        // Name of the type of report
        public string Name { get; private set; } = null!;

        // Default constructor initializing the TypeReport object
        public TypeReport() {}

        // Constructor to initialize the TypeReport with a specific name
        public TypeReport(string name)
        {
            Name = name;
        }

        // Navigation property representing a collection of associated reports
        public virtual ICollection<Report> Reports { get; } = [];
    }
}
