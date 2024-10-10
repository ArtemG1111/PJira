

using MediatR;

namespace PJira.Application.Assignments.Commands.UpdateAssignment
{
    public class UpdateAssignmentCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }

    }
}
