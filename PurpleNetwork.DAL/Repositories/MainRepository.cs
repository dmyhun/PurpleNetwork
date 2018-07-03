using PurpleNetwork.DAL.EF;
using PurpleNetwork.DAL.Entities;
using PurpleNetwork.DAL.Interfaces;
using PurpleNetwork.DAL.Infrastucture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurpleNetwork.DAL.Repositories
{
    public class MainRepository : IMainRepository
    {
        private MainContext mainContext;

        public MainRepository()
        {
            this.mainContext = new MainContext();
        }

        /* Publications */

        public void AddPublication(Publication publication)
        {
            mainContext.Publications.Add(publication);
            mainContext.SaveChanges();               
        }

        public IEnumerable<Publication> GetPublicationsByUserEmail(string email, PaginationInfo p)
        {
            int startIndex = (p.BlockNumber - 1) * p.BlockSize;

            var publicationes = mainContext.Publications.Where(u => u.UserId == email).OrderByDescending(e => e.Date)
                .Skip(startIndex).Take(p.BlockSize).ToList();

            return publicationes;
        }

        public IEnumerable<Publication> GetPublicationsFromSubscriptions(string email, PaginationInfo p)
        {
            int startIndex = (p.BlockNumber - 1) * p.BlockSize;

            var publications = new List<Publication>();

            var subscriptions = this.GetSubscriptions(email);

            var result = mainContext.Publications.OrderByDescending(e => e.Date).Where(pub => subscriptions.Any(s => pub.UserId == s))
                .Skip(startIndex).Take(p.BlockSize).ToList();

            return result;

        }

        /* Subscription */

        public void AddSubscription(string follower, string following)
        {
            if (this.GetSubscriptions(follower).Any(e => e == following) != true)
            {
                Subscription subscription = new Subscription
                {
                    Id = Guid.NewGuid(),
                    FollowerUserEmail = follower,
                    FollowingUserEmail = following
                };

                mainContext.Subscriptions.Add(subscription);
                mainContext.SaveChanges();        
            }             
        }

        public IEnumerable<string> GetSubscriptions(string follower)
        {
            IEnumerable<string> subsList = mainContext.Subscriptions.Where(u => u.FollowerUserEmail == follower).Select(s => s.FollowingUserEmail);

            return subsList;
        }

        public void DeleteSubscription(string follower, string following)
        {
            Subscription subscription = mainContext.Subscriptions.Where(e => e.FollowerUserEmail == follower && e.FollowingUserEmail == following).FirstOrDefault();

            if (subscription != null)
            {
                mainContext.Subscriptions.Remove(subscription);
                mainContext.SaveChanges();
            }
        }

    }
}