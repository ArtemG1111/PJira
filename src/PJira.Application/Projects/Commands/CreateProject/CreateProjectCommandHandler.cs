

using MediatR;
using PJira.Application.Common.Interfaces;
using PJira.Core.Models;

namespace PJira.Application.Projects.Commands.CreateProject
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Guid>
    {
        private readonly IPJiraDbContext _dbContext;

        public CreateProjectCommandHandler(IPJiraDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
        {
            var project = new Project
            {
                Name = command.Name,
            };

            _dbContext.Projects.Add(project);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return project.Id;
        }
    }
}
