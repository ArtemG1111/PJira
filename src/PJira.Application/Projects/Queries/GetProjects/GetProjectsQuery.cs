
using MediatR;
using PJira.Core.Models;

namespace PJira.Application.Projects.Queries.GetProjects
{
    public class GetProjectsQuery : IRequest<List<Project>>
    {

    }
}
