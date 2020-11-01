using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stp.Data.Entities
{
    [Table("Test")]
    public class Test
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DurationMinutes { get; set; }
        public int Points { get; set; }
        public int TasksCount { get; set; }
        public string Skills { get; set; }
        public int Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
