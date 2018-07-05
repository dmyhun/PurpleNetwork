using System.ComponentModel.DataAnnotations;

namespace PurpleNetwork.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "This is a required field")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Incorrect email adress")]
        [DataType(DataType.EmailAddress)]
        public string EmailLogin { get; set; }
        [Required(ErrorMessage = "This is a required field")]
        [DataType(DataType.Password)]
        public string PasswordLogin { get; set; }
    }
}