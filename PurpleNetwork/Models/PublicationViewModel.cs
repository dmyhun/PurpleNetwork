using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PurpleNetwork.Models
{
    public class PublicationViewModel
    {
        public string UserId { get; set; }

        public string UserAvatarUrl { get; set; }

        public string UserFirstName{ get; set; }

        public string UserLastName{ get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public DateTime Date { get; set; }
    }
}