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

namespace Stp.TestingApi.Contracts
{
    
    public class CreateTaskCommand
    {
        public long TaskCategoryId { get; set; }
        
        public string Name { get; set; }
        public int Points { get; set; }
        public int DurationMinutes { get; set; }
        public TaskType Type { get; set; }
        public TaskComplexity Complexity { get; set; }        

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
