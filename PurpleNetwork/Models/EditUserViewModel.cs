using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PurpleNetwork.Models
{
    public class EditUserViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Description { get; set; }

        public HttpPostedFileBase UploadAvatar { get; set; }
                
        public string TestVal { get; set; }

    }
}