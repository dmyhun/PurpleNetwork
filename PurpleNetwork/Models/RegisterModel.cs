using System;
using System.ComponentModel.DataAnnotations;

namespace PurpleNetwork.Models
{
    public class RegisterModel
    {
        [Required]
        public string EmailRegister { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PasswordRegister { get; set; }

        [Required]
        [Compare("PasswordRegister", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}