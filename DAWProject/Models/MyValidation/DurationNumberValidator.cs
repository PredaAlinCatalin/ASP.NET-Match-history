using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DAWProject.Models.MyValidation
{
    public class DurationNumberValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var game = (Game)validationContext.ObjectInstance;
            int duration = game.Duration;
            bool cond = true;

            if (duration < 4 || duration > 1000 || duration % 2 == 1)
                cond = false;

            return cond ? ValidationResult.Success : new ValidationResult("This is not a valid game duration!");
        }
    }
}