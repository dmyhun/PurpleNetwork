using PurpleNetwork.DAL.Infrastucture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleNetwork.DAL.Interfaces
{
    public interface IUserRepository
    {
        ApplicationUser GetUserByEmail(string email);

        IEnumerable<ApplicationUser> GetUsers();

        IEnumerable<ApplicationUser> GetUsers(Func<ApplicationUser, bool> predicate);

        void EditUser(ApplicationUser user);
    }
}
