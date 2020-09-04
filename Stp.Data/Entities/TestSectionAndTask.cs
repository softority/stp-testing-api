using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stp.Data.Entities
{
    [Table(nameof(TestSectionAndTask))]
    public class TestSectionAndTask
    {
        public long Id { get; set; }
        public int TaskPosition { get; set; }

        [ForeignKey(nameof(TestSection))]
        public long TestSectionId { get; set; }
        [ForeignKey(nameof(Task))]
        public long TaskId { get; set; }

        public virtual TestSection TestSection { get; set; }
        public virtual StpTask Task { get; set; }
    }
}
