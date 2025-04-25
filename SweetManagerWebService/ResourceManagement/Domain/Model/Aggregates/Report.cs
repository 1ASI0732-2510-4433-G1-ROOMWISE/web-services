using SweetManagerWebService.IAM.Domain.Model.Aggregates;
using SweetManagerWebService.ResourceManagement.Domain.Model.Commands;
using SweetManagerWebService.ResourceManagement.Domain.Model.Entities;

namespace SweetManagerWebService.ResourceManagement.Domain.Model.Aggregates
{
    public partial class Report
    {
        public int Id { get; }
        public int TypesReportsId { get; set; }
        public int AdminsId { get; set; }
        public int WorkersId { get; set; }
        public string FileUrl { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual Admin Admin { get; } = null!;
        public virtual TypeReport TypeReport { get; } = null!;
        public virtual Worker Worker { get; } = null!;

        public Report()
        {
            this.TypesReportsId = 0;
            this.AdminsId = 0;
            this.WorkersId = 0;
            this.FileUrl = string.Empty;
            this.Title = string.Empty;
            this.Description = string.Empty;
        }

        public Report(int typesReportsId, int adminsId, int workersId, string fileUrl, string title, string description)
        {
            this.TypesReportsId = typesReportsId;
            this.AdminsId = adminsId;
            this.WorkersId = workersId;
            this.FileUrl = fileUrl;
            this.Title = title;
            this.Description = description;
        }

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