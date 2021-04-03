using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WAD._8392.DAl.Validation
{
    //attribute for date of birth validation
    public class DateOfBirthValidation : ValidationAttribute
    {
        //minimum valid age
        private int _ageLimit;
        public DateOfBirthValidation(int limit)
        {
            _ageLimit = limit;
        }
        public override bool IsValid(object value)
        {
            //value is date of birth
            //if date of birth plus minimum age is more than todays date, it means that user is younger than required minimum age
            DateTime dateTime = Convert.ToDateTime(value);
            if (dateTime.AddYears(_ageLimit) > DateTime.Now)
            {
                return false;
            }
            return true;
        }
    }
}
