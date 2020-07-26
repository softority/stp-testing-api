using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stp.Data.Entities
{
    [Table(nameof(Exercise))]
    public class Exercise
    {
        public long Id { get; set; }
        public string TestSectionId { get; set; }
        public long ExerciseCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Type { get; set; }
        public int Points { get; set; }
        public int DurationMinutes { get; set; }
        public int Complexity { get; set; }
        public string CodeEditorUrl { get; set; }
        public bool IsDeleted { get; set; }
    }
}
