

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PJira.Application.Common.Interfaces;
using PJira.Application.DTOs;
using PJira.Core.Models;

namespace PJira.Application.Projects.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto>
    {
        private readonly IPJiraDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetProjectByIdQueryHandler(IPJiraDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ProjectDto> Handle(GetProjectByIdQuery query, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<ProjectDto>(await _dbContext.Projects.Include(a => a.Assignments)
                .FirstOrDefaultAsync(i => i.Id == query.Id));

            if(project == null)
            {
                throw new Exception();
            }

            return project;
        }
    }
}
