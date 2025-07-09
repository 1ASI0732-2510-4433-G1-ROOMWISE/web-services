using SweetManagerWebService.IAM.Domain.Model.Aggregates;
using SweetManagerWebService.ResourceManagement.Domain.Model.Commands;
using SweetManagerWebService.ResourceManagement.Domain.Model.Entities;

namespace SweetManagerWebService.ResourceManagement.Domain.Model.Aggregates
{
    // Represents a report entity with associated properties and constructors
    public partial class Report
    {
        // Unique identifier for the report
        public int Id { get; }
        
        // Foreign key to the type of report
        public int TypesReportsId { get; set; }
        
        // Foreign key to the admin who created the report
        public int AdminsId { get; set; }
        
        // Foreign key to the worker associated with the report
        public int WorkersId { get; set; }
        
        // URL of the file associated with the report
        public string FileUrl { get; set; } = null!;
        
        // Title of the report
        public string Title { get; set; } = null!;
        
        // Description of the report
        public string Description { get; set; } = null!;

        // Navigation property to the associated admin
        public virtual Admin Admin { get; } = null!;
        
        // Navigation property to the associated type report
        public virtual TypeReport TypeReport { get; } = null!;
        
        // Navigation property to the associated worker
        public virtual Worker Worker { get; } = null!;

        // Default constructor initializing properties to default values
        public Report()
        {
            this.TypesReportsId = 0;
            this.AdminsId = 0;
            this.WorkersId = 0;
            this.FileUrl = string.Empty;
            this.Title = string.Empty;
            this.Description = string.Empty;
        }

        // Constructor to initialize report with specific values
        public Report(int typesReportsId, int adminsId, int workersId, string fileUrl, string title, string description)
        {
            this.TypesReportsId = typesReportsId;
            this.AdminsId = adminsId;
            this.WorkersId = workersId;
            this.FileUrl = fileUrl;
            this.Title = title;
            this.Description = description;
        }

        // Constructor to create a report from a command object (CreateReportCommand)
        public Report(CreateReportCommand command)
        {
            this.TypesReportsId = command.TypesReportsId;
            this.AdminsId = command.AdminsId;
            this.WorkersId = command.WorkersId;
            this.FileUrl = command.FileUrl;
            this.Title = command.Title;
            this.Description = command.Description;
        }
    }
}
