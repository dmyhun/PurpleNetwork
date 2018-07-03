using PurpleNetwork.DAL.Infrastucture;
using PurpleNetwork.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleNetwork.DAL.Repositories
{
    public class UserRepository: IUserRepository
    {
        private ApplicationContext applicationContext;

        public UserRepository()
        {
            this.applicationContext = new ApplicationContext();
        }

        public void EditUser(ApplicationUser user)
        {
            applicationContext.Entry(user).State = EntityState.Modified;
            applicationContext.SaveChanges();
        }

        public ApplicationUser GetUserByEmail(string email)
        {
            ApplicationUser user = applicationContext.Users.Where(u => u.Email == email).FirstOrDefault();
            return user;
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            return applicationContext.Users;
        }

        public IEnumerable<ApplicationUser> GetUsers(Func<ApplicationUser, bool> predicate)
        {
            return applicationContext.Users.Where(predicate);
        }
    }
}
