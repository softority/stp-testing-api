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

namespace Stp.TestingApi.Contracts
{
    public class TaskDto
    {
        public TaskSummaryDto TaskSummary { get; set; }
        public MultichoiceTaskInfoDto MultichoiceTaskInfo { get; set; }
        public CodingTaskInfoDto CodingTaskInfo { get; set; }
        //public List<SkillDto> Skills { get; set; }
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
