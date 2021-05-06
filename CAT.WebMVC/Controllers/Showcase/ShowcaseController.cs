using CAT.Models.Showcase_Models;
using CAT.Services.Showcase_Service;
using Microsoft.AspNet.Identity;
using System;
using PagedList;
using PagedList.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CAT.WebMVC.Controllers.Showcase
{
    [Authorize]
    public class ShowcaseController : Controller
    {
        // GET: Showcase/Index
        public ActionResult Index()
        {
            var service = CreateShowcaseService();
            var model = service.GetShowcaseIndex();
            return View(model);
        }

        // GET: Showcase/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Showcase/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShowcaseCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var service = CreateShowcaseService();

            if (service.CreateShowcase(model))
            {
                TempData["SaveResult"] = "Collection successfully created!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Collection could not be created. Please make sure that all required input fields are populated.");

            return View(model);
        }

        // GET: Showcase/Details/{id}
        public ActionResult Details(int id)
        {
            var service = CreateShowcaseService();
            var model = service.GetShowcaseDetails(id);
            return View(model);
        }

        // GET: Showcase/Edit
        public ActionResult Edit(int id)
        {
            var service = CreateShowcaseService();
            var detail = service.GetShowcaseDetails(id);

            var model =
                new ShowcaseEdit 
                {
                    ShowcaseId = detail.ShowcaseId,
                    ShowcaseName = detail.ShowcaseName,
                    ShowcaseDescription = detail.ShowcaseDescription,                   
                };
            return View(model);
        }

        // POST: Showcase/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ShowcaseEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.ShowcaseId != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateShowcaseService();

            if (service.EditShowcase(model))
            {
                TempData["SaveResult"] = "Collection successfully modified!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Collection could not be modified! Please make sure that all required input fields are populated.");

            return View(model);
        }

        // GET: Showcase/Delete
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateShowcaseService();
            var model = service.GetShowcaseDetails(id);
            return View(model);
        }

        // POST: Showcase/Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ShowcaseDelete(int id)
        {
            var service = CreateShowcaseService();

            service.DeleteShowcase(id);

            TempData["SaveResult"] = "Showcase was successfully deleted.";

            return RedirectToAction("Index");
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