using SweetManagerWebService.IAM.Domain.Model.Entities.Assignments;

namespace SweetManagerWebService.IAM.Domain.Model.Entities.Roles
{
    public partial class WorkerArea(string name)
    {
        public int Id { get; }

        public string Name { get; set; } = name.ToUpper();

        public virtual ICollection<AssignmentWorker> AssignmentsWorkers { get; } = [];
    }
}