using CAT.Contexts.Data;
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
    [Authorize]
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

        // GET: Moment/Details/{id}
        public ActionResult Details(int id)
        {
            var service = CreateMomentService();
            var model = service.GetMomentDetails(id);
            return View(model);
        }

        // GET: Moment/Edit/{id}
        public ActionResult Edit(int id)
        {
            var service = CreateMomentService();
            var detail = service.GetMomentDetails(id);
            var playerList = service.PlayersList();
            ViewData["Players"] = playerList;

            var model =
                new MomentEdit
                {
                    MomentId = detail.MomentId,
                    PlayerId = detail.PlayerId,
                    MomentCategory = detail.MomentCategory,
                    DateOfMoment = detail.DateOfMoment,
                    MomentSet = detail.MomentSet,
                    MomentSeries = detail.MomentSeries,
                    MomentSerialNumber = detail.MomentSerialNumber,
                    MomentCirculatingCount = detail.MomentCirculatingCount,
                    MomentTier = detail.MomentTier,
                    MomentMint = detail.MomentMint,
                    PurchasedInPack = detail.PurchasedInPack,
                    AmountInPack = detail.AmountInPack,
                    PurchasedForPrice = detail.PurchasedForPrice
                };

            return View(model);
        }

        // POST: Moment/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MomentEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.MomentId != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateMomentService();

            if (service.EditMoment(model))
            {
                TempData["SaveResult"] = "Moment was successfully updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Moment could not be updated. Please make sure that all required input fields are populated.");

            return View(model);
        }

        // GET: Moment/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateMomentService();
            var model = service.GetMomentDetails(id);
            return View(model);
        }

        // POST: Moment/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult MomentDelete(int id)
        {
            var service = CreateMomentService();

            service.DeleteMoment(id);

            TempData["SaveResult"] = "Moment was successfully deleted.";

            return RedirectToAction("Index");
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