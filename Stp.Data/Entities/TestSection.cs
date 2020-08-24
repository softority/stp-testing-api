using System;
using System.Collections.Generic;
using System.Text;

namespace Stp.Data.Entities
{

    public class TestSection
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Position { get; set; }
        public long TestId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<TestSectionAndTask> TestSectionsAndTasks { get; set; }
    }
}
