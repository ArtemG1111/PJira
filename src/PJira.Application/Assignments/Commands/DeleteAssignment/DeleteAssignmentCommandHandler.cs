

using MediatR;
using Microsoft.EntityFrameworkCore;
using PJira.Application.Common.Interfaces;

namespace PJira.Application.Assignments.Commands.DeleteAssignment
{
    class DeleteAssignmentCommandHandler : IRequestHandler<DeleteAssignmentCommand>
    {
        private readonly IPJiraDbContext _dbContext;
        public DeleteAssignmentCommandHandler(IPJiraDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteAssignmentCommand command, CancellationToken cancellationToken)
        {
            var assignment = await _dbContext.Assignments
                .FirstOrDefaultAsync(a => a.Id == command.Id);

            if (assignment == null)
            {
                throw new Exception();
            }

            _dbContext.Assignments.Remove(assignment);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
