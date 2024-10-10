

using MediatR;
using PJira.Core.Models;

namespace PJira.Application.Assignments.Queries.GetAssignment
{
    public class GetAssignmentsQuery : IRequest<List<Assignment>>
    {

    }
}
