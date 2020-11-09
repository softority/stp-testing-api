using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stp.Data.Entities
{
    [Table(nameof(TestCategory))]
    public class TestCategory
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual TestCategory Parent { get; set; }
        public int Position { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<TestCategoryAndTest> TestCategoryAndTests { get; set; }
    }
}
