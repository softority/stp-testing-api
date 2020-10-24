using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Stp.TestingApi.Validation
{
    public class EnumMemberValueAttribute : ValidationAttribute
    {
        public Type EnumType { get; set; }
        public EnumMemberValueAttribute(Type enumType)
        {
            EnumType = enumType;
        }
        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(name);
        }
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            if (EnumType == null)
            {
                return false;
            }

            //if (!value.GetType().IsEnum)
            //    return true;

            if (!Enum.IsDefined(EnumType, value))
            {
                ErrorMessage = $"Invalid value for the enum '{EnumType.Name}'";
                return false;
            }

            return true;
        }
    }
}
