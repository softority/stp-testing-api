﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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
        [Required]
        public string? Name { get; set; }

        [Range(1, 100)]
        public int Points { get; set; }

        [Range(1, 180)]
        public int DurationMinutes { get; set; }

        [Required]
        [EnumMemberValue(typeof(TaskType))]
        public TaskType Type { get; set; }

        [Required]
        [EnumMemberValue(typeof(TaskComplexity))]
        public TaskComplexity Complexity { get; set; }        

        [Required]
        public List<SkillStateDto>? Skills { get; set; }
    }
    public class SkillStateDto //: IValidatableObject
    {
        public long? Id { get; set; }

        [StringLength(512, MinimumLength = 1)]
        [Required]
        public string? Name { get; set; }

        [Required]
        [EnumMemberValue(typeof(SkillState))]
        public SkillState State { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Name.Length > 56 && State != SkillState.New)
        //    {
        //        yield return new ValidationResult("Invalid state");
        //    }
        //    if (Name.Length > 156 && State != SkillState.Removed)
        //    {
        //        yield return new ValidationResult("");
        //    }
        //}
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
