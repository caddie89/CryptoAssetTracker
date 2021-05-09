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
using CAT.Contracts.Showcase_Contract;

namespace CAT.WebMVC.Controllers.Showcase
{
    [Authorize]
    public class ShowcaseController : Controller
    {
        private Guid _userId;

        private readonly IShowcaseService _showcaseService;

        public ShowcaseController(IShowcaseService showcaseService)
        {
            _showcaseService = showcaseService;
        }

        // GET: Showcase/Index
        public ActionResult Index()
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            var model = _showcaseService.GetShowcaseIndex(userId);
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

            model.OwnerId = Guid.Parse(User.Identity.GetUserId());

            if (_showcaseService.CreateShowcase(model))
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
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            var model = _showcaseService.GetShowcaseDetails(id, userId);
            return View(model);
        }

        // GET: Showcase/Edit
        public ActionResult Edit(int id)
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            var detail = _showcaseService.GetShowcaseDetails(id, userId);

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
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            if (!ModelState.IsValid)
                return View(model);

            if (model.ShowcaseId != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            if (_showcaseService.EditShowcase(model, userId))
            {
                TempData["SaveResult"] = "Collection successfully modified!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Collection could not be modified. Please make sure that all required input fields are populated.");

            return View(model);
        }

        // GET: Showcase/Delete
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            var model = _showcaseService.GetShowcaseDetails(id, userId);
            return View(model);
        }

        // POST: Showcase/Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ShowcaseDelete(int id)
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            _showcaseService.DeleteShowcase(id, userId);

            TempData["SaveResult"] = "Collection was successfully removed.";

            return RedirectToAction("Index");
        }
    }
}