using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using PJira.Application.Assignments.Commands.CreateAssignment;
using PJira.Application.Assignments.Commands.DeleteAssignment;
using PJira.Application.Assignments.Commands.UpdateAssignment;
using PJira.Application.Common.Interfaces;
using PJira.Core.Enums;
using PJira.Core.Models;

namespace PJira.Application.Tests.AssignmentTests.CommandsTests
{
    public class AssignmnetCommandTests
    {
        [Fact]
        public async Task Should_Create_Assignment()
        {
            var mockDbContext = new Mock<IPJiraDbContext>();

            var mockDbSet = new Mock<DbSet<Assignment>>();

            mockDbContext.Setup(a => a.Assignments).Returns(mockDbSet.Object);

            var command = new CreateAssignmentCommand
            {
                Title = "test title",
                Description = "test description",
                Status = AssignmentStatus.New
            };

            var handler = new CreateAssignmentCommandHandler(mockDbContext.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsType<Guid>(result);

            mockDbSet.Verify(d => d.Add(It.IsAny<Assignment>()), Times.Once);

            mockDbContext.Verify(d => d.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Should_Delete_Assignment()
        {
            List<Assignment> assignments = new List<Assignment>
            {
                new Assignment{Id = Guid.NewGuid()},
                new Assignment{Id = Guid.NewGuid()}
            };

            var mockDbContext = new Mock<IPJiraDbContext>();


            var command = new DeleteAssignmentCommand { Id = assignments[0].Id };

            mockDbContext.Setup(s => s.Assignments).ReturnsDbSet(assignments);

            mockDbContext.Setup(s => s.Assignments.Remove(assignments[0]))
                .Callback(() => assignments.Remove(assignments[0]));

            var handler = new DeleteAssignmentCommandHandler(mockDbContext.Object);

            await handler.Handle(command, CancellationToken.None);

            mockDbContext.Verify(a => a.Assignments.Remove(It.IsAny<Assignment>()), Times.Once);
            mockDbContext.Verify(s => s.SaveChangesAsync(It.IsAny<CancellationToken>()));

            Assert.Equal(1, assignments.Count);

        }
        [Fact]
        public async Task Should_Update_Assignment()
        {
            var mockDbContext = new Mock<IPJiraDbContext>();


            var assignmetnId = Guid.NewGuid();

            var existingAssignment = new Assignment
            {
                Id = assignmetnId,
                Title = "Test",
                Description = "Test",
                Status = AssignmentStatus.New
            };

            List<Assignment> assignments = new List<Assignment> { existingAssignment };

            var command = new UpdateAssignmentCommand
            {
                Id = assignmetnId,
                Title = "Test Title",
                Description = "Test Description",
                Status = AssignmentStatus.Testing
            };

            mockDbContext.Setup(a => a.Assignments).ReturnsDbSet(assignments);

            mockDbContext.Setup(a => a.Assignments.Update(It.IsAny<Assignment>()));

            var handler = new UpdateAssignmentCommandHandler(mockDbContext.Object);

            await handler.Handle(command, CancellationToken.None);

            Assert.Equal("Test Title", existingAssignment.Title);
            Assert.Equal("Test Description", existingAssignment.Description);
            Assert.True(existingAssignment.Status == AssignmentStatus.Testing);

            mockDbContext.Verify(s => s.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);


        }
    }
}
