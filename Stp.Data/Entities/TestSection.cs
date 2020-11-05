using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stp.Data.Entities
{
    [Table(nameof(TestSection))]
    public class TestSection
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }

        [ForeignKey(nameof(Test))]
        public long TestId { get; set; }
      
        public bool IsDeleted { get; set; }

        public virtual Test Test { get; set; }
        public virtual ICollection<TestSectionAndTask> TestSectionsAndTasks { get; set; }
    }
}
