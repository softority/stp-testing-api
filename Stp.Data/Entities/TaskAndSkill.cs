using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stp.Data.Entities
{
    [Table("TaskAndSkill")]
    public class TaskAndSkill
    {
        public long Id { get; set; }
        [ForeignKey(nameof(StpTask))]
        public long TaskId { get; set; }
        [ForeignKey(nameof(StpSkill))]
        public long SkillId { get; set; }

        public virtual StpTask Task { get; set; }
        public virtual StpSkill Skill { get; set; }
    }
}
