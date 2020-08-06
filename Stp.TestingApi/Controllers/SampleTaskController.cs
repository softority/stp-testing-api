using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stp.Data.Entities;

namespace Stp.TestingApi.Controllers
{
    public enum TaskType
    {
        Multichoice,
        Coding
    }
    public enum TaskComplexity
    {
        Low,
        Medium,
        High
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
        public List<string> Skills { get; set; }
        public TaskComplexity Complexity { get; set; }
    }
    public class MultichoiceTaskInfoDto
    {
        public string Question { get; set; }
        public List<MultichoiceTaskAnswerDto> Answers { get; set; }
    }
    public class CodingTaskInfoDto
    {
        public string Question { get; set; }
        public string CodingToolUrl { get; set; }
    }

    public class MultichoiceTaskAnswerDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsCorrect { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class SampleTaskController : ControllerBase
    {
        [HttpGet(nameof(GetSampleTasks))]
        public IEnumerable<TaskDto> GetSampleTasks()
        {
            var skills = new List<string>() { "Arrays", ".NET", "C#", "Generics" };
            var mcInfo = new MultichoiceTaskInfoDto()
            {
                Question = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                Answers = new List<MultichoiceTaskAnswerDto>()
                {
                    new MultichoiceTaskAnswerDto() { Id = 1, Name = "Addition will produce result 1."},
                    new MultichoiceTaskAnswerDto() { Id = 2, Name = "Result of addition is system-dependent."},
                    new MultichoiceTaskAnswerDto() { Id = 3, Name = "Program will generate run-time exception."},
                    new MultichoiceTaskAnswerDto() { Id = 4, Name = "Compiler will report an error: Operator \'+\' is not defined for types T and int.", IsCorrect = true },
                    new MultichoiceTaskAnswerDto() { Id = 5, Name = "None of the above."}
                }
            };
            var res = new List<TaskDto>()
            {
                new TaskDto()
                { 
                    TaskSummary = new TaskSummaryDto() 
                    {
                        Id = 1,
                        Name = "Array dimensions question with generics constraints and basics v01 MR",
                        Type = TaskType.Multichoice,
                        Skills = skills,
                        Complexity = TaskComplexity.High,
                        DurationMinutes = 7,
                        Points = 10
                    } ,
                    MultichoiceTaskInfo = mcInfo
                },
                new TaskDto()
                {
                    TaskSummary = new TaskSummaryDto()
                    {
                        Id = 2,
                        Name = "Array dimensions question with generics constraints and basics v02 MR",
                        Type = TaskType.Multichoice,
                        Skills = skills,
                        Complexity = TaskComplexity.Medium,
                        DurationMinutes = 3,
                        Points = 5
                    } ,
                    MultichoiceTaskInfo = mcInfo
                },
                new TaskDto()
                {
                    TaskSummary = new TaskSummaryDto()
                    {
                        Id = 3,
                        Name = "Array dimensions question with generics constraints and basics v03 MR",
                        Type = TaskType.Multichoice,
                        Skills = skills,
                        Complexity = TaskComplexity.Low,
                        DurationMinutes = 1,
                        Points = 2
                    } ,
                    MultichoiceTaskInfo = mcInfo
                },
                new TaskDto()
                {
                    TaskSummary = new TaskSummaryDto()
                    {
                        Id = 4,
                        Name = "Array dimensions question with generics constraints and basics v04 MR",
                        Type = TaskType.Multichoice,
                        Skills = skills,
                        Complexity = TaskComplexity.High,
                        DurationMinutes = 7,
                        Points = 10
                    } ,
                    MultichoiceTaskInfo = mcInfo
                }
            };

            return res;
        }
    }
}
