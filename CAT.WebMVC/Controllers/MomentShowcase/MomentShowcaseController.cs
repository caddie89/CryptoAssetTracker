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
        // GET: MomentShowcase/Create
        public ActionResult Create()
        {
            var service = CreateMomentShowcaseService();
            var momentList = service.SelectMoment();
            ViewData["Moments"] = momentList;
            var showcaseList = service.SelectShowcase();
            ViewData["Showcases"] = showcaseList;

            //var moments = service.SelectMoments();
            //ViewBag.Moments = moments;

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
                TempData["SaveResult"] = "Asset successfully added to Collection!";
                return RedirectToAction("Index", "Showcase");
            }

            ModelState.AddModelError("", "Asset could not be added to Collection. Please make sure all input fields are populated.");

            // service.CreateMomentShowcase(model);

            return View(model);
        }

        // GET: MomentShowcase/Details/{id}/{id}
        public ActionResult Details(int momentId, int showcaseId)
        {
            var service = CreateMomentShowcaseService();
            var model = service.GetMomentShowcaseDetails(momentId, showcaseId);
            return View(model);
        }

        // GET: MomentShowcase/Delete/{id}/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int momentId, int showcaseId)
        {
            var service = CreateMomentShowcaseService();
            var model = service.GetMomentShowcaseDetails(momentId, showcaseId);
            return View(model);
        }

        // POST: MomentShowcase/Delete/{id}/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ShowcaseDelete(int momentId, int showcaseId)
        {
            var service = CreateMomentShowcaseService();
            service.DeleteMomentShowcase(momentId, showcaseId);

            TempData["SaveResult"] = "Asset successfully removed from Collection!";

            return RedirectToAction("Index", "Showcase");
        }

        // Helper Method
        private MomentShowcaseService CreateMomentShowcaseService()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MomentShowcaseService(userId);
            return service;
        }
    }
}