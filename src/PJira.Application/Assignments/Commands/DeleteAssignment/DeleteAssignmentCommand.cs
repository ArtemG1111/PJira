

using MediatR;

namespace PJira.Application.Assignments.Commands.DeleteAssignment
{
    public class DeleteAssignmentCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
