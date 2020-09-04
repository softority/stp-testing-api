using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace Stp.Data.Enums
{
    public enum TaskType
    {
        [Display(Name = "Multichoice")]
        Multichoice,
        [Display(Name = "Coding")]
        Coding
    }
}
