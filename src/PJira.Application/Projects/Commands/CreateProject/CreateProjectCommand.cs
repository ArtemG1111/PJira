

using MediatR;
using PJira.Core.Models;

namespace PJira.Application.Projects.Commands.CreateProject
{
    public class CreateProjectCommand : IRequest<Guid>
    {
        public string Name { get; set; }
    }
}
