using CAT.Models.Moment_Models;
using CAT.Services.Moment_Service;
using CAT.Services.SoldMoment_Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CAT.WebMVC.Controllers.SoldMoment
{
    public class SoldMomentController : Controller
    {
        // GET: SoldMoment/Index
        public ActionResult Index()
        {
            var service = CreateSoldMomentService();
            var model = service.GetSoldMomentIndex();
            return View(model);
        }

        // GET: SoldMoment/Create/{id}
        public ActionResult Create(int id)
        {
            var momentService = CreateMomentService();
            var detail = momentService.GetMomentDetails(id);

            var model =
                new SoldMomentCreate
                {
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
                    PackPrice = detail.PackPrice,
                    IndividualMomentPrice = detail.IndividualMomentPrice
                };

            return View(model);
        }

        // POST: SoldMoment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SoldMomentCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var service = CreateSoldMomentService();

            if (service.CreateSoldMoment(model))
            {
                TempData["SaveResult"] = "Moment added to Sold List.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Moment could not be added to Sold List. Please make sure that all required input fields are populated.");

            return View(model);
        }

        // GET: SoldMoment/Details/{id}
        public ActionResult Details(int id)
        {
            var service = CreateSoldMomentService();
            var model = service.GetSoldMomentDetails(id);
            return View(model);
        }

        // Helper Method
        private SoldMomentService CreateSoldMomentService()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SoldMomentService(userId);
            return service;
        }

        // Helper Method
        private MomentService CreateMomentService()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            var service = new MomentService(userId);
            return service;
        }
    }
}