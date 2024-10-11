

namespace PJira.Application.DTOs
{
    public class AssignmentDto
    {
        Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
    }
}
