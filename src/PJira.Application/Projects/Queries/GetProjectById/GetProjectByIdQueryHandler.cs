

using MediatR;
using Microsoft.EntityFrameworkCore;
using PJira.Application.Common.Interfaces;
using PJira.Core.Models;

namespace PJira.Application.Projects.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Project>
    {
        private readonly IPJiraDbContext _dbContext;

        public GetProjectByIdQueryHandler(IPJiraDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Project> Handle(GetProjectByIdQuery query, CancellationToken cancellationToken)
        {
            var project = await _dbContext.Projects.Include(a => a.Assignments)
                .FirstOrDefaultAsync(i => i.Id == query.Id);

            if(project == null)
            {
                throw new Exception();
            }

            return project;
        }
    }
}
