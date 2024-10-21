

using MediatR;
using Microsoft.EntityFrameworkCore;
using PJira.Application.Common.Interfaces;
using PJira.Core.Models;

namespace PJira.Application.Projects.Queries.GetProjects
{
    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, List<Project>>
    {
        private readonly IPJiraDbContext _dbContext;

        public GetProjectsQueryHandler(IPJiraDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Project>> Handle(GetProjectsQuery query, CancellationToken cancellationToken)
        {
            return await _dbContext.Projects.ToListAsync(cancellationToken);
        }
    }
}
