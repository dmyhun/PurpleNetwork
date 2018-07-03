using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PurpleNetwork.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public IEnumerable<PublicationViewModel> Publications { get; set; }

        public bool IsMe { get; set; }

        public bool IsMySubscription { get; set; }

    }
}