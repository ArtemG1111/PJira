

using MediatR;
using PJira.Application.DTOs;


namespace PJira.Application.Projects.Queries.GetProjectById
{
    public class GetProjectByIdQuery : IRequest<ProjectDto>
    {
        public Guid Id { get; set; }
    }
}
