using System;
using System.ComponentModel.DataAnnotations;

namespace PurpleNetwork.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "The First Name field is required for the reqistration")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "The lenght of this field has to be from 3 to 20 symbols")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The Last Name field is required for the reqistration")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "The lenght of this field has to be from 3 to 20 symbols")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required for the reqistration")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Incorrect email adress")]
        [DataType(DataType.EmailAddress)]
        public string EmailRegister { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "The lenght of this field has to be from 6 to 20 symbols")]
        [DataType(DataType.Password)]
        public string PasswordRegister { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [Compare("PasswordRegister", ErrorMessage = "These passwords differ from each other")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}