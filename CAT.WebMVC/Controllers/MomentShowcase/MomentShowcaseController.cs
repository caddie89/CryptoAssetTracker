using CAT.Models.MomentShowcase_Models;
using CAT.Services.MomentShowcase_Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CAT.WebMVC.Controllers.MomentShowcase
{
    [Authorize]
    public class MomentShowcaseController : Controller
    {
        // GET: MomentShowcase/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: MomentShowcase/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MomentShowcase/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MomentShowcaseCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var service = CreateMomentShowcaseService();

            if (service.CreateMomentShowcase(model))
            {
                TempData["SaveResult"] = "Showcase was added to Moment.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Showcase could not be added to Moment.");

            return View(model);
        }

        // GET: MomentShowcase/Details

        // Helper Method
        private MomentShowcaseService CreateMomentShowcaseService()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MomentShowcaseService(userId);
            return service;
        }
    }
}