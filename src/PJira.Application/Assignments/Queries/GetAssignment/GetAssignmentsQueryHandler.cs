

using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PJira.Application.Common.Interfaces;
using PJira.Application.DTOs;
using PJira.Core.Models;

namespace PJira.Application.Assignments.Queries.GetAssignment
{
    public class GetAssignmentsQueryHandler : IRequestHandler<GetAssignmentsQuery, List<AssignmentDto>>
    {
        private readonly IPJiraDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetAssignmentsQueryHandler(IPJiraDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<AssignmentDto>> Handle(GetAssignmentsQuery query, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<AssignmentDto>>(await _dbContext.Assignments.ToListAsync());
        }
    }
}
