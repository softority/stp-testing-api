using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Stp.Data.Entities
{
    [Table("Task")]
    public class StpTask
    {
        public long Id { get; set; }

        [ForeignKey(nameof(Category))]
        public long CategoryId { get; set; }
        public virtual TaskCategory Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskType Type { get; set; }
        public int Points { get; set; }
        public int DurationMinutes { get; set; }
        public TaskComplexity Complexity { get; set; }
        public string CodeEditorUrl { get; set; }
        public int Position { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<MultichoiceTaskAnswer> MultichoiceAnswers { get; set; }
    }

    public enum TaskComplexity
    {
        [Display(Name = "Low")]
        Low,
        [Display(Name = "Medium")]
        Medium,
        [Display(Name = "High")]
        High
    }

    public enum TaskType
    {
        [Display(Name = "Multichoice")]
        Multichoice,
        [Display(Name = "Coding")]
        Coding
    }
}
