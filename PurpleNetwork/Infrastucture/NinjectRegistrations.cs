using Ninject.Modules;
using PurpleNetwork.DAL.Interfaces;
using PurpleNetwork.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PurpleNetwork.Infrastucture
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserRepository>().To<UserRepository>();
            Bind<IMainRepository>().To<MainRepository>();
        }
    }
}