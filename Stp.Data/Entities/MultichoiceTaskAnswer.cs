using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Stp.Data.Entities
{
    [Table(nameof(MultichoiceTaskAnswer))]
    public class MultichoiceTaskAnswer
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(Task))]
        public long TaskId { get; set; }
        public virtual StpTask Task { get; set; }

    }
}
