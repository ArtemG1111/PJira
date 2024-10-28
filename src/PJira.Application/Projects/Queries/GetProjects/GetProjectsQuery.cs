
using MediatR;
using PJira.Application.DTOs;

namespace PJira.Application.Projects.Queries.GetProjects
{
    public class GetProjectsQuery : IRequest<List<ProjectDto>>
    {

    }
}
