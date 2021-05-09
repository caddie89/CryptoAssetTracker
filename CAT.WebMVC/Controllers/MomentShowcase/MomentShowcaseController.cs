using CAT.Contracts.MomentShowcase_Contract;
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
        private Guid _userId;

        private readonly IMomentShowcaseService _momentShowcaseService;
        public MomentShowcaseController(IMomentShowcaseService momentShowcaseService)
        {
            _momentShowcaseService = momentShowcaseService;
        }

        // GET: MomentShowcase/Create
        public ActionResult Create()
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            var momentList = _momentShowcaseService.SelectMoment(userId);
            TempData["MomentId"] = momentList;
            var showcaseList = _momentShowcaseService.SelectShowcase(userId);
            TempData["ShowcaseId"] = showcaseList;

            return View();
        }

        // POST: MomentShowcase/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MomentShowcaseCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.OwnerId = Guid.Parse(User.Identity.GetUserId());

            if (_momentShowcaseService.CreateMomentShowcase(model) == false)
            {
                TempData["AlreadyAdded"] = "Oops! Collection already contains this Asset. Please select a different Asset to add.";
                return RedirectToAction("Create", "MomentShowcase");
            }

            if (_momentShowcaseService.CreateMomentShowcase(model))
            {
                TempData["SaveResult"] = "Asset successfully added to Collection!";
                return RedirectToAction("Index", "MomentShowcase");
            }
            
            ModelState.AddModelError("", "Asset could not be added to Collection. Please make sure all input fields are populated.");

            return View(model);
        }

        // GET: MomentShowcase/Details/{id}/{id}
        public ActionResult Details(int momentId, int showcaseId)
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            var model = _momentShowcaseService.GetMomentShowcaseDetails(momentId, showcaseId, userId);
            return View(model);
        }

        // GET: MomentShowcase/Delete/{id}/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int momentId, int showcaseId)
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            var model = _momentShowcaseService.GetMomentShowcaseDetails(momentId, showcaseId, userId);
            return View(model);
        }

        // POST: MomentShowcase/Delete/{id}/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ShowcaseDelete(int momentId, int showcaseId)
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            _momentShowcaseService.DeleteMomentShowcase(momentId, showcaseId, userId);

            TempData["SaveResult"] = "Asset successfully removed from Collection!";

            return RedirectToAction("Index", "Showcase");
        }
    }
}