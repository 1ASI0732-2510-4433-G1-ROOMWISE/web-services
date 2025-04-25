using SweetManagerWebService.ResourceManagement.Domain.Model.Aggregates;

namespace SweetManagerWebService.ResourceManagement.Domain.Model.Entities
{
    public partial class TypeReport
    {
        public int Id { get; }
        public string Name { get; private set; } = null!;

        public TypeReport() {}

        public TypeReport(string name)
        {
            Name = name;
        }
        
        public virtual ICollection<Report> Reports { get; } = [];
    }
}