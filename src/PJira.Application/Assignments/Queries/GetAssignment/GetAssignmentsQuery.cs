

using MediatR;
using PJira.Application.DTOs;



namespace PJira.Application.Assignments.Queries.GetAssignment
{
    public class GetAssignmentsQuery : IRequest<List<AssignmentDto>>
    {

    }
}
