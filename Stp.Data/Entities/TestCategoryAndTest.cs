using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stp.Data.Entities
{
    [Table(nameof(TestCategoryAndTest))]
    public class TestCategoryAndTest
    {
        public long Id { get; set; }
        public int TestPosition { get; set; }

        [ForeignKey(nameof(TestCategory))]
        public long TestCategoryId { get; set; }

        [ForeignKey(nameof(Test))]
        public long TestId { get; set; }

        public virtual TestCategory TestCategory { get; set; }
        public virtual Test Test { get; set; }
    }
}
