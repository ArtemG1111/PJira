

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PJira.Application.Common.Interfaces;
using PJira.Application.DTOs;


namespace PJira.Application.Projects.Queries.GetProjects
{
    public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, List<ProjectDto>>
    {
        private readonly IPJiraDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetProjectsQueryHandler(IPJiraDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<ProjectDto>> Handle(GetProjectsQuery query, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<ProjectDto>>(await _dbContext.Projects.
                Include(a=>a.Assignments).ToListAsync(cancellationToken));
        }
    }
}
