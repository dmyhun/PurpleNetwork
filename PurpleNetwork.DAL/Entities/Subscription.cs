using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleNetwork.DAL.Entities
{
    public class Subscription
    {
        public Guid Id { get; set; }

        public string FollowerUserEmail { get; set; }

        public string FollowingUserEmail { get; set; }
    }
}
