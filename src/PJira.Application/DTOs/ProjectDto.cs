

namespace PJira.Application.DTOs
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<AssignmentDto> Assignments { get; set; }
    }
}
