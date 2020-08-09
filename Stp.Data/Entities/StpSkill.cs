using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stp.Data.Entities
{
    [Table("Skill")]
    public class StpSkill
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TaskAndSkill> TaskAndSkills { get; set; }
    }
}
