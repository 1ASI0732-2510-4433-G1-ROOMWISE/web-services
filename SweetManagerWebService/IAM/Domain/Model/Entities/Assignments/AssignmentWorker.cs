using SweetManagerWebService.IAM.Domain.Model.Aggregates;
using SweetManagerWebService.IAM.Domain.Model.Commands.Assignments;
using SweetManagerWebService.IAM.Domain.Model.Entities.Roles;

namespace SweetManagerWebService.IAM.Domain.Model.Entities.Assignments
{
    public partial class AssignmentWorker
    {
        public int Id { get; }
        public int WorkersAreasId { get; private set; }
        public int? WorkersId { get; private set; } 
        public int? AdminsId { get; private set; } 
        public DateTime StartDate { get; private set; } 
        public DateTime FinalDate { get; private set; }
        public string State { get; private set; } 

        public virtual Admin Admin { get; } = null!;
        public virtual Worker Worker { get; } = null!;
        public virtual WorkerArea WorkerArea { get; } = null!;

        public AssignmentWorker(int workersAreasId, int? workersId, int? adminsId, DateTime startDate, DateTime finalDate, string state) 
        {
            WorkersAreasId = workersAreasId;
            WorkersId = workersId;
            AdminsId = adminsId;
            StartDate = startDate;
            FinalDate = finalDate;
            State = state;
        }
        
        public AssignmentWorker(CreateAssignmentWorkerCommand command)
        {
            WorkersAreasId = command.WorkersAreasId;
            WorkersId = command.WorkersId;
            AdminsId = command.AdminsId;
            StartDate = command.StartDate;
            FinalDate = command.FinalDate;
            State = command.State.ToUpper();
        }
        
        public AssignmentWorker() { }
    }
}