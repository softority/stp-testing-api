using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Stp.Data.Entities
{
    [Table(nameof(Exercise))]
    public class Exercise
    {
        public long Id { get; set; }
        public long TestSectionId { get; set; }
        public long ExerciseCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ExerciseType Type { get; set; }
        public int Points { get; set; }
        public int DurationMinutes { get; set; }
        public ExerciseComplexity Complexity { get; set; }
        public string CodeEditorUrl { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<MultichoiceExerciseAnswer> MultichoiceAnswers { get; set; }
    }

    public enum ExerciseComplexity
    {
        [Display(Name = "Low")]
        Low,
        [Display(Name = "Medium")]
        Medium,
        [Display(Name = "High")]
        High
    }

    public enum ExerciseType
    {
        [Display(Name = "Multichoice")]
        Multichoice,
        [Display(Name = "Coding")]
        Coding
    }
}
