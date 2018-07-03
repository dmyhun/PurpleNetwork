using PurpleNetwork.DAL.Entities;
using PurpleNetwork.DAL.Infrastucture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleNetwork.DAL.Interfaces
{
    public interface IMainRepository
    {
        void AddPublication(Publication publication);

        IEnumerable<Publication> GetPublicationsByUserEmail(string email, PaginationInfo p);

        IEnumerable<Publication> GetPublicationsFromSubscriptions(string email, PaginationInfo p);

        void AddSubscription(string follower, string following);

        IEnumerable<string> GetSubscriptions(string follower);

        void DeleteSubscription(string follower, string following);
    }
}
