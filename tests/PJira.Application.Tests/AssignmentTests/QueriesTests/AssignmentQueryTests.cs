

using AutoMapper;
using Moq;
using Moq.EntityFrameworkCore;
using PJira.Application.Assignments.Queries.GetAssignment;
using PJira.Application.Common.Interfaces;
using PJira.Application.DTOs;
using PJira.Core.Models;

namespace PJira.Application.Tests.AssignmentTests.QueriesTests
{
    public class AssignmentQueryTests
    {
        [Fact]
        public async Task Should_GetAll_Assignments()
        {
            List<Assignment> assignments = new List<Assignment>()
            {
                new Assignment {Id = Guid.NewGuid()},
                new Assignment {Id = Guid.NewGuid()}
            };

            var mockDbContext = new Mock<IPJiraDbContext>();            
            
            mockDbContext.Setup(a => a.Assignments).ReturnsDbSet(assignments);

            var mockMapper = new Mock<IMapper>();
            
            var query = new GetAssignmentsQuery();

            mockMapper.Setup(m => m.Map<List<AssignmentDto>>(assignments))
                .Returns(new List<AssignmentDto>
                { 
                    new AssignmentDto {Id = assignments[0].Id},
                    new AssignmentDto {Id = assignments[1].Id}
                });

            var handler = new GetAssignmentsQueryHandler(mockDbContext.Object, mockMapper.Object);

            var result = await handler.Handle(query, CancellationToken.None);
            
            Assert.NotNull(result);

            Assert.Equal(2, result.Count);

            mockDbContext.Verify(a=>a.Assignments, Times.Once());
        }
    }
}
