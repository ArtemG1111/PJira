

using MediatR;
using PJira.Application.Common.Interfaces;
using PJira.Core.Models;

namespace PJira.Application.Assignments.Commands.CreateTask
{
    public class CreateAssignmentCommandHandler : IRequestHandler<CreateAssignmentCommand, Guid>
    {
        private readonly IPJiraDbContext _dbContext;
        public CreateAssignmentCommandHandler(IPJiraDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateAssignmentCommand command, CancellationToken cancellationToken)
        {
            var assignment = new Assignment
            {
                Title = command.Title,
                Description = command.Description,
                Status = command.Status
            };

            _dbContext.Assignments.Add(assignment);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return assignment.Id;
        }
    }
}
