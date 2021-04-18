using CAT.Services.Showcase_Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CAT.WebMVC.Controllers.Showcase
{
    [Authorize]
    public class ShowcaseController : Controller
    {
        // GET: Showcase/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Showcase/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create()
        //{

        //}

        // GET: Showcase/Index
        public ActionResult Index()
        {
            var service = CreateShowcaseService();
            var model = service.GetShowcaseIndex();

            return View(model);
        }

        // Helper Method
        private ShowcaseService CreateShowcaseService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ShowcaseService(userId);
            return service;
        }
    }
}