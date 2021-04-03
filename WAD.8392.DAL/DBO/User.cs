using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WAD._8392.DAl.Validation;

namespace WAD._8392.DAL.DBO
{
    public class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage ="First name is required")]
        [MinLength(3, ErrorMessage = "First name should have at least 3 characters")]
        public string FirstName { get; set; }
        [MinLength(3, ErrorMessage = "Last name should have at least 3 characters")]
        public string LastName { get; set; }
        //required data annotation is added to DateOfBirth and DateOfBirth is made nullable for more convenient model validation
        [Required(ErrorMessage ="Date of birth is required")]
        //users younger that 18 cannot register
        [DateOfBirthValidation(18, ErrorMessage = "You should be at least 18 years old")]
        public DateTime? DateOfBirth { get; set; }
        //the value of this property is set automatically in controller
        public DateTime DateRegistered { get; set; }

        [Required(ErrorMessage ="Phone number is required")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        //username should be unique
        [Required(ErrorMessage ="Username is required")]
        [MinLength(5, ErrorMessage = "Username should have at least 5 characters")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Invalid email address")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password is required")]
        [MinLength(8, ErrorMessage = "Password should be minimum 8 characters long")]
        public string Password { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
