using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WAD._8392.DAL.Validation;

namespace WAD._8392.DAL.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        [MinLength(3)]
        public string FirstName { get; set; }
        [MinLength(3)]
        public string LastName { get; set; }
        [DateOfBirthValidation(18, ErrorMessage ="Should be at least 18 y.o.")]
        public DateTime DateOfBirth { get; set; }
       
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        [MinLength(5)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage ="Password should be minimum 8 characters long")]
        public string Password { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
