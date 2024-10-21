

using PJira.Core.Enums;

namespace PJira.Core.Models
{
    public class Assignment
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public AssignmentStatus Status { get; set; }
        public Project Project { get; set; }
        public Guid ProjectId { get; set; }
    }
}
