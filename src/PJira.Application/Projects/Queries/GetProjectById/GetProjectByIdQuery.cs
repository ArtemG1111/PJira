

using MediatR;
using PJira.Core.Models;

namespace PJira.Application.Projects.Queries.GetProjectById
{
    public class GetProjectByIdQuery : IRequest<Project>
    {
        public Guid Id { get; set; }
    }
}
