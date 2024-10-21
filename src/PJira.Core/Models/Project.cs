

namespace PJira.Core.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<Assignment> Assignments { get; set; }
    }
}
