using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using PurpleNetwork.DAL.Entities;
using System.Data.Entity;

namespace PurpleNetwork.DAL.EF
{
    public class MainContext : DbContext
    {
        public DbSet<Publication> Publications { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        public MainContext() : base("UserDb")
        {
            
        }
    }
}
