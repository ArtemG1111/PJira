

using Moq;
using Moq.EntityFrameworkCore;
using PJira.Application.Common.Interfaces;
using PJira.Application.Projects.Queries.GetProjectById;
using PJira.Application.Projects.Queries.GetProjects;
using PJira.Core.Models;

namespace PJira.Application.Tests.ProjectTests.QueriesTests
{
    public class ProjectQueriesTests
    {
        [Fact]
        public async Task Should_GetAll_Projects()
        {
            List<Project> projects =  new List<Project>
            {
                new Project{Id = Guid.NewGuid()},
                new Project{Id = Guid.NewGuid()}
            };

            var mockDbContext = new Mock<IPJiraDbContext>();

            mockDbContext.Setup(p=>p.Projects).ReturnsDbSet(projects);

            var query = new GetProjectsQuery();

            var handler = new GetProjectsQueryHandler(mockDbContext.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);

            Assert.Equal(2, result.Count);

            mockDbContext.Verify(p => p.Projects, Times.Once());

        }
        [Fact]
        public async Task Should_Get_Project_By_Id()
        {
            var mockDbContext = new Mock<IPJiraDbContext>();

            var projectId = Guid.NewGuid();

            List<Project> projects = new List<Project> { new Project { Id = projectId } };

            mockDbContext.Setup(g => g.Projects).ReturnsDbSet(projects);

            var query = new GetProjectByIdQuery { Id = projectId };

            var handler = new GetProjectByIdQueryHandler(mockDbContext.Object);

            var result = await handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal(projects[0], result);

            mockDbContext.Verify(p => p.Projects, Times.Once());

        }
    }
}
