using CAT.Contracts.Moment_Contract;
using CAT.Contracts.SoldMoment_Contract;
using CAT.Models.Moment_Models;
using CAT.Models.SoldMoment_Models;
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
    [Authorize]
    public class SoldMomentController : Controller
    {
        private Guid _userId;

        private readonly IMomentService _momentService;
        private readonly ISoldMomentService _soldMomentService;

        public SoldMomentController(IMomentService momentService, ISoldMomentService soldMomentService)
        {
            _momentService = momentService;
            _soldMomentService = soldMomentService;
        }

        // GET: SoldMoment/Index
        public ActionResult Index()
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            var model = _soldMomentService.GetSoldMomentIndex(userId);

            var count = _soldMomentService.MomentCount(userId);
            var totalValue = _soldMomentService.MomentTotalValue(userId);
            var profitLoss = _soldMomentService.MomentTotalProfitLoss(userId);
            var ROI = _soldMomentService.MomentROI(userId);

            ViewData["AssetCount"] = count;
            ViewData["AssetTotalValue"] = totalValue;
            ViewData["ProfitLoss"] = profitLoss;
            ViewData["ROI"] = ROI;

            return View(model);
        }

        // GET: SoldMoment/Create/{id}
        public ActionResult Create(int id)
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            var detail = _momentService.GetMomentDetails(id, userId);

            var model =
                new SoldMomentCreate
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

            model.OwnerId = Guid.Parse(User.Identity.GetUserId());

            if (_soldMomentService.CreateSoldMoment(model))
            {
                TempData["SaveResult"] = "Asset sale recorded! Recommended that Asset be removed from Asset List.";
                return RedirectToAction("Delete", "Moment", new { id = model.MomentId });
            }

            ModelState.AddModelError("", "Asset sale could not be recored. Please make sure that all required input fields are populated.");

            return View(model);
        }

        // GET: SoldMoment/Details/{id}
        public ActionResult Details(int id)
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            var model = _soldMomentService.GetSoldMomentDetails(id, userId);
            return View(model);
        }

        // GET: SoldMoment/Edit/{id}
        public ActionResult Edit(int id)
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            var detail = _soldMomentService.GetSoldMomentDetails(id, userId);

            var model =
                new SoldMomentEdit
                {
                    SoldMomentId = detail.SoldMomentId,
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
                    PackPrice = detail.PackPrice,
                    IndividualMomentPrice = detail.IndividualMomentPrice,
                    SoldForAmount = detail.SoldForAmount
                };

            return View(model);
        }

        // POST: SoldMoment/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SoldMomentEdit model)
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            if (!ModelState.IsValid)
                return View(model);

            if (model.SoldMomentId != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            if (_soldMomentService.EditSoldMoment(model, userId))
            {
                TempData["SaveResult"] = "Sale price for Sold Asset successfully modified!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Sale price for Sold Asset could not be modified. Please make sure that all required input fields are populated.");

            return View(model);
        }

        // GET: SoldMoment/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            var model = _soldMomentService.GetSoldMomentDetails(id, userId);
            return View(model);
        }

        // POST: SoldMoment/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult SoldMomentDelete(int id)
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            _soldMomentService.DeleteSoldMoment(id, userId);

            TempData["SaveResult"] = "Sold Asset successfully removed!";

            return RedirectToAction("Index");
        }
    }
}