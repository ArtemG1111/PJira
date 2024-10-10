

using MediatR;
using Microsoft.EntityFrameworkCore;
using PJira.Application.Common.Interfaces;

namespace PJira.Application.Assignments.Commands.UpdateAssignment
{
    public class UpdateAssignmentCommandHandler : IRequestHandler<UpdateAssignmentCommand>
    {
        private readonly IPJiraDbContext _dbContext;
        public UpdateAssignmentCommandHandler(IPJiraDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Handle(UpdateAssignmentCommand command, CancellationToken cancellationToken)
        {
            var assignment = await _dbContext.Assignments
                .FirstOrDefaultAsync(a => a.Id == command.Id);

            if (assignment == null)
            {
                throw new Exception();
            }

            assignment.Title = command.Title;
            assignment.Description = command.Description;
            assignment.Status = command.Status;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
