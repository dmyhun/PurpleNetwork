using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PurpleNetwork.DAL.EF;
using PurpleNetwork.DAL.Entities;
using PurpleNetwork.Models;

namespace PurpleNetwork.Infrastucture
{
    public static class ModelMapper
    {
        public static PublicationViewModel MapPublications(ApplicationUser u, Publication p)
        {
            PublicationViewModel publicationVM = new PublicationViewModel
            {                
                UserAvatarUrl = u.ImageUrl,
                UserFirstName = u.FirstName,
                UserLastName = u.LastName,
                UserId = p.UserId,
                Name = p.Name,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                Date = p.Date
            };
            
        return publicationVM;
        }

        public static UserSearchViewModel MapUserSearch(ApplicationUser user)
        {
            UserSearchViewModel result = new UserSearchViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Description = user.Description,
                ImageUrl = user.ImageUrl,
                Email = user.Email
            };

            return result;
        }        

        public static UserViewModel MapUser(ApplicationUser user)
        {
            UserViewModel result = new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ImageUrl = user.ImageUrl,
                Description = user.Description,
                Email = user.Email
            };

            return result;
        }

        public static ApplicationUser MapUserRegister(RegisterModel model)
        {
            ApplicationUser result = new ApplicationUser
            {
                UserName = model.EmailRegister,
                Email = model.EmailRegister,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ImageUrl = "https://innmind.com/assets/placeholders/no_avatar-3d6725770296b6a1cce653a203d8f85dcc5298945b71fa7360e3d9aa4a3fc054.svg",
                Description = "Hello world! I'd like to chat and have a lot of friends :)"
            };
            
            return result;
        }

        public static EditUserViewModel MapEditUser(ApplicationUser user)
        {
            EditUserViewModel result = new EditUserViewModel
            {               
                FirstName = user.FirstName,
                LastName = user.LastName,
                Description = user.Description
            };

            return result;
        }
    }
}