using CAT.Models.Moment_Models;
using CAT.Services.Moment_Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CAT.WebMVC.Controllers.Moment
{
    public class MomentController : Controller
    {
        // GET: Moment/Index
        public ActionResult Index()
        {
            var service = CreateMomentService();
            var model = service.GetMomentIndex();
            return View(model);
        }

        // GET: Moment/Create
        public ActionResult Create()
        {
            var service = CreateMomentService();
            var model = service.SelectPlayers();
            ViewData["Players"] = model;

            return View();
        }

        // POST: Moment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MomentCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var service = CreateMomentService();

            if (service.CreateMoment(model))
            {
                TempData["SaveResult"] = "Moment was successfully created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Moment could not be created. Please make sure that all required input fields are populated.");

            return View(model);
        }

        // Helper Method
        private MomentService CreateMomentService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MomentService(userId);
            return service;
        }
    }
}