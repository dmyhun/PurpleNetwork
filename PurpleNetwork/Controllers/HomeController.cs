using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PurpleNetwork.Models;
using PurpleNetwork.DAL.EF;
using PurpleNetwork.DAL.Entities;
using System.Data.Entity;
using PurpleNetwork.DAL.Repositories;
using PurpleNetwork.DAL.Interfaces;


using PurpleNetwork.Infrastucture;
using System.IO;
using PurpleNetwork.DAL.Infrastucture;

namespace PurpleNetwork.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IUserRepository userRepository;
        private IMainRepository mainRepository;
       // private PaginationInfo pagination = new PaginationInfo { BlockNumber = }

        public HomeController(IUserRepository userRepository, IMainRepository mainRepository)
        {

                this.userRepository = userRepository;
                this.mainRepository = mainRepository;      
        }

        public ActionResult Info(string email)
        {
            var me = User.Identity.Name;
            if (String.IsNullOrEmpty(email))
                email = me;
            ApplicationUser user = this.userRepository.GetUserByEmail(email);
            if (user == null)
                return HttpNotFound("User with this email " + email + " wasn't found");
            List<PublicationViewModel> publicationsVM = new List<PublicationViewModel>();           
            UserViewModel userVM = ModelMapper.MapUser(user);
            userVM.IsMe = (email == me);
            userVM.IsMySubscription = (email == me) ? false : mainRepository.GetSubscriptions(me).Any(e => e == email);           
            return View(userVM);
        }

        [ChildActionOnly]
        public ActionResult PublicationList(List<PublicationViewModel> model)
        {
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Publication_InfinateScroll(string email, int blockNumber)
        {
            PaginationInfo pagination = new PaginationInfo { BlockSize = 5, BlockNumber = blockNumber };
            List<PublicationViewModel> publicationsVM = new List<PublicationViewModel>();
            var publications = mainRepository.GetPublicationsByUserEmail(email, pagination).OrderByDescending(e => e.Date);
            var user = userRepository.GetUserByEmail(email);
            foreach (Publication p in publications)
            {
                PublicationViewModel publicationVM = ModelMapper.MapPublications(user, p);
                publicationsVM.Add(publicationVM);
            }
            JsonModel jsonModel = new JsonModel
            {
                NoMoreData = publications.Count() < pagination.BlockSize,
                HTMLString = RenderPartialViewToString("PublicationList", publicationsVM)
            };
            return Json(jsonModel);
        }

        public ActionResult Subscriptions(string follower)
        {
            if (follower == null)
                follower = User.Identity.Name;
            List<SubscibersViewModel> subscriptions = new List<SubscibersViewModel>();
            var subsList = mainRepository.GetSubscriptions(follower);
            foreach (var sub in subsList)
            {
                ApplicationUser user = this.userRepository.GetUserByEmail(sub);
                SubscibersViewModel u = new SubscibersViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Description = user.Description,
                    ImageUrl = user.ImageUrl,
                    Email = user.Email
                };
                subscriptions.Add(u);
            }
            return View(subscriptions);
        }

        public ActionResult Feed()
        {   
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult SearchList(List<UserSearchViewModel> Model)
        {
            return PartialView(Model);
        }

        [HttpPost]
        public ActionResult Search_InfinateScroll(string searchKeyWords, int blockNumber)
        {
            PaginationInfo pagination = new PaginationInfo { BlockSize = 6, BlockNumber = blockNumber };
            int startIndex = (pagination.BlockNumber - 1) * pagination.BlockSize;
            IEnumerable<ApplicationUser> users = (String.IsNullOrWhiteSpace(searchKeyWords)) ? userRepository.GetUsers().Distinct().Skip(startIndex).Take(pagination.BlockSize).ToList() : userRepository
                 .GetUsers(u => u.FirstName.Contains(searchKeyWords) || u.LastName.Contains(searchKeyWords) || u.UserName.Contains(searchKeyWords))
                 .Distinct().Skip(startIndex).Take(pagination.BlockSize).ToList();
            List<UserSearchViewModel> _users = new List<UserSearchViewModel>();
            foreach (var user in users)
            {
                UserSearchViewModel u = ModelMapper.MapUserSearch(user);
                _users.Add(u);
            }
            JsonModel jsonModel = new JsonModel();
            jsonModel.NoMoreData = _users.Count < pagination.BlockSize;
            jsonModel.HTMLString = RenderPartialViewToString("SearchList", _users);
            return Json(jsonModel);
        }
        
        public ActionResult Messages()
        {
            return View();
        }

        public ActionResult Settings()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPublication(PublicationViewModel publication, HttpPostedFileBase uploadFile)
        {
            Publication p = new Publication
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                UserId = publication.UserId,
                Name = publication.Name,
                Description = publication.Description
            };
            if (uploadFile != null && uploadFile.ContentLength > 0)
            {
                string path = "/Temp/Publications/" + p.Id.ToString() + Path.GetExtension(uploadFile.FileName);
                string filePath = Path.Combine(Server.MapPath(path));
                uploadFile.SaveAs(filePath);
                p.ImageUrl = path;
            }
            mainRepository.AddPublication(p);
            return RedirectToAction("Info");
        }

        [HttpPost]
        public ActionResult AddSubscription(string following)
        {
            var me = User.Identity.Name;
            mainRepository.AddSubscription(me, following);
            return RedirectToAction("Subscriptions", new { follower = me });
        }

        [HttpPost]
        public ActionResult DeleteSubscription(string following)
        {
            var me = User.Identity.Name;
            mainRepository.DeleteSubscription(me, following);
            return RedirectToAction("Subscriptions", new { follower = me });
        }

        [HttpPost]
        public ActionResult Feed_InfinateScroll(int blockNumber)
        {
            PaginationInfo pagination = new PaginationInfo { BlockSize = 5, BlockNumber = blockNumber };            
            IEnumerable<Publication> subsPublications = mainRepository.GetPublicationsFromSubscriptions(User.Identity.Name, new PaginationInfo { BlockNumber = blockNumber, BlockSize = 5 });
            List<PublicationViewModel> publicationsVM = new List<PublicationViewModel>();
            foreach (var p in subsPublications)
            {
                ApplicationUser user = this.userRepository.GetUserByEmail(p.UserId);
                PublicationViewModel publicationVM = ModelMapper.MapPublications(user, p);
                publicationsVM.Add(publicationVM);
            }            
            JsonModel jsonModel = new JsonModel
            {
                NoMoreData = subsPublications.Count() < pagination.BlockSize,
                HTMLString = RenderPartialViewToString("PublicationList", publicationsVM)
            };
            return Json(jsonModel);
        }

        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult =
                ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext
                (ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}