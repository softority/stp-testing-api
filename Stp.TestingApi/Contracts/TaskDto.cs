using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Stp.Data.Entities;

namespace Stp.TestingApi.Contracts
{
    public class TaskDto
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }
        public int DurationMinutes { get; set; }

        public TaskType Type { get; set; }
        public TaskComplexity Complexity { get; set; }
        public ICollection<MultichoiceAnswerDto> MultichoiceAnswers { get; set; }

        public TaskDto()
        {
            MultichoiceAnswers = new List<MultichoiceAnswerDto>();
        }
    }

}
