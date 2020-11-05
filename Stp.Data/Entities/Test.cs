using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using Stp.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stp.Data.Entities
{
    [Table(nameof(Test))]
    public class Test
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        #region Removed
        //public int DurationMinutes { get; set; }
        //public int Points { get; set; }
        //public int TasksCount { get; set; }
        //public string Skills { get; set; }
        #endregion
        public TestStatus Status { get; set; }
        public bool IsDeleted { get; set; }

        public virtual List<TestSection> TestSections { get; set; }
    }
}
