

using MediatR;
using PJira.Application.Common.Interfaces;

namespace PJira.Application.Projects.Commands.DeleteProject
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
    {
        private readonly IPJiraDbContext _dbContext;

        public DeleteProjectCommandHandler(IPJiraDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task Handle(DeleteProjectCommand command, CancellationToken cancellationToken)
        {
            var project = _dbContext.Projects.FirstOrDefault(p => p.Id == command.Id);

            if (project == null)
            {
                throw new Exception("Project not found");
            }

            _dbContext.Projects.Remove(project);

            await _dbContext.SaveChangesAsync(cancellationToken);
        } 
    }
}
