using MediatR;
using PJira.Core.Enums;

namespace PJira.Application.Assignments.Commands.CreateAssignment
{
    public class CreateAssignmentCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public AssignmentStatus Status { get; set; }
        public Guid ProjectId { get; set; }
    }
}
