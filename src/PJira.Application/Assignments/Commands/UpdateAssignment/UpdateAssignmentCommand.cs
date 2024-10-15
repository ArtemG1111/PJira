

using MediatR;
using PJira.Core.Enums;

namespace PJira.Application.Assignments.Commands.UpdateAssignment
{
    public class UpdateAssignmentCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public AssignmentStatus Status { get; set; }

    }
}
