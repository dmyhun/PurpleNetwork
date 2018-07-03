using System.ComponentModel.DataAnnotations;

namespace PurpleNetwork.Models
{
    public class LoginModel
    {
        [Required]
        public string EmailLogin { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PasswordLogin { get; set; }
    }
}