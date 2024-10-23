

using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using PJira.Application.Common.Interfaces;
using PJira.Application.Projects.Commands.CreateProject;
using PJira.Application.Projects.Commands.DeleteProject;
using PJira.Application.Projects.Commands.UpdateProject;
using PJira.Core.Models;

namespace PJira.Application.Tests.ProjectTests.CommandsTests
{
    public class ProjectCommandTests
    {
        [Fact]
        public async Task Should_Create_Project()
        {
            var mockDbContext = new Mock<IPJiraDbContext>();

            var mockDbSet = new Mock<DbSet<Project>>();

            mockDbContext.Setup(p => p.Projects).Returns(mockDbSet.Object);

            var command = new CreateProjectCommand
            {
                Name = "Test Name"
            };

            var handler = new CreateProjectCommandHandler(mockDbContext.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Guid>(result);

            mockDbContext.Verify(a => a.Projects.Add(It.IsAny<Project>()), Times.Once);
            mockDbContext.Verify(s=>s.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async Task Should_Delete_Project()
        {
            List<Project> projects = new List<Project>
            {
                new Project{Id = Guid.NewGuid()},
                new Project{Id = Guid.NewGuid()}
            };

            var mockDbContext = new Mock<IPJiraDbContext>();

            var command = new DeleteProjectCommand { Id = projects[0].Id };

            mockDbContext.Setup(p => p.Projects).ReturnsDbSet(projects);

            mockDbContext.Setup(d => d.Projects.Remove(projects[0]))
                .Callback(() => projects.Remove(projects[0]));

            var handler = new DeleteProjectCommandHandler(mockDbContext.Object);

            await handler.Handle(command, CancellationToken.None);

            mockDbContext.Verify(p=>p.Projects.Remove(It.IsAny<Project>()),Times.Once);
            mockDbContext.Verify(s => s.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
        [Fact]
        public async Task Should_Update_Project()
        {
            var mockDbContext = new Mock<IPJiraDbContext>();

            var projectId = Guid.NewGuid();

            var exictingProject = new Project
            {
                Id = projectId,
                Name = "Test"
            };

            List<Project> projects = new List<Project> { exictingProject };

            var command = new UpdateProjectCommand
            {
                Id = projectId,
                Name = "Test Name"
            };

            mockDbContext.Setup(p => p.Projects).ReturnsDbSet(projects);

            mockDbContext.Setup(p => p.Projects.Update(It.IsAny<Project>()));

            var handler = new UpdateProjectCommandHandler(mockDbContext.Object);

            await handler.Handle(command, CancellationToken.None);

            Assert.Equal("Test Name", exictingProject.Name);

            mockDbContext.Verify(s => s.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
