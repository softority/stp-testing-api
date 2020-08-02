using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Stp.Data.Entities
{
    [Table(nameof(MultichoiceExerciseAnswer))]
    public class MultichoiceExerciseAnswer
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsDeleted { get; set; }
        public long ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }

    }
}
