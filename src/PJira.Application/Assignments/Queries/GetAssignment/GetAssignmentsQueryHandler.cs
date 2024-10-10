

using MediatR;
using Microsoft.EntityFrameworkCore;
using PJira.Application.Common.Interfaces;
using PJira.Core.Models;

namespace PJira.Application.Assignments.Queries.GetAssignment
{
    public class GetAssignmentsQueryHandler : IRequestHandler<GetAssignmentsQuery, List<Assignment>>
    {
        private readonly IPJiraDbContext _dbContext;
        public GetAssignmentsQueryHandler(IPJiraDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Assignment>> Handle(GetAssignmentsQuery query, CancellationToken cancellationToken)
        {
            return await _dbContext.Assignments.ToListAsync();
        }
    }
}
