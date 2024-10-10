using MediatR;

namespace PJira.Application.Assignments.Commands.CreateTask
{
    public class CreateAssignmentCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }

    }
}
