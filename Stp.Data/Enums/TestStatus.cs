using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stp.Data.Enums
{
    public enum TestStatus
    {
        [Display(Name = "Created")]
        Created,
        [Display(Name = "On review")]
        OnReview,
        [Display(Name = "Publised")]
        Publised
    }
}
