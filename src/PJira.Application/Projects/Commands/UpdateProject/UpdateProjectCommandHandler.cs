

using MediatR;
using PJira.Application.Common.Interfaces;

namespace PJira.Application.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
    {
        private readonly IPJiraDbContext _dbContext;

        public UpdateProjectCommandHandler(IPJiraDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(UpdateProjectCommand command, CancellationToken cancellationToken)
        {
            var project = _dbContext.Projects.FirstOrDefault(i => i.Id == command.Id);

            if (project == null)
            {
                throw new Exception();
            }

            project.Name = command.Name;
            project.IsActive = command.IsActive;

            _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
