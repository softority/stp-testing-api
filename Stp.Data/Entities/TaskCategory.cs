using System.ComponentModel.DataAnnotations.Schema;

namespace Stp.Data.Entities
{
    [Table(nameof(TaskCategory))]
    public class TaskCategory
    {
        public long Id { get; set; }
        [ForeignKey(nameof(Parent))]
        public long ParentId { get; set; }
        public TaskCategory Parent { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public bool IsDeleted { get; set; }
    }
}
