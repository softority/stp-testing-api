using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Stp.Data.Entities;
using Stp.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Stp.TestingApi.Contracts
{
    using Validation;
    public class CreateTaskCommand
    {
        [Range(1, long.MaxValue)]
        public long TaskCategoryId { get; set; }
        
        [StringLength(512, MinimumLength = 1)]
        public string Name { get; set; }

        [Range(1, 100)]
        public int Points { get; set; }

        [Range(1, 180)]
        public int DurationMinutes { get; set; }

        [Required]
        [EnumMemberValue(typeof(TaskType))]
        public TaskType Type { get; set; }

        public TaskComplexity Complexity { get; set; }        

        [Required]
        public List<SkillStateDto> Skills { get; set; }
    }
    public class SkillStateDto
    {
        public long? Id { get; set; }
        public string Name { get; set; }

        public SkillState State { get; set; }
    }
    public enum SkillState
    {
        Added = 0,
        Removed = 1,
        New = 2
    }
    public class TaskDto
    {
        public TaskSummaryDto TaskSummary { get; set; }
        public MultichoiceTaskInfoDto MultichoiceTaskInfo { get; set; }
        public CodingTaskInfoDto CodingTaskInfo { get; set; }
    }

    public class TaskSummaryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public TaskType Type { get; set; }
        public int Points { get; set; }
        public int Position { get; set; }
        public int DurationMinutes { get; set; }        
        public TaskComplexity Complexity { get; set; }
        public List<SkillDto> Skills { get; set; }
    }
    

}
