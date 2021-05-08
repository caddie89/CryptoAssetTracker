using CAT.Contexts.Data;
using CAT.Contracts.Moment_Contract;
using CAT.Models.Moment_Models;
using CAT.Services.Moment_Service;
using Microsoft.AspNet.Identity;
using PagedList;
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
        private Guid _userId;

        private readonly IMomentService _momentService;

        public MomentController(IMomentService momentService)
        {
            _momentService = momentService;
        }

        // GET: Moment/Index
        public ActionResult Index()
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            var model = _momentService.GetMomentIndex(userId);

            var count = _momentService.MomentCount(userId);
            var totalValue = _momentService.MomentTotalValue(userId);
            var profitLoss = _momentService.MomentTotalProfitLoss(userId);
            var ROI = _momentService.MomentROI(userId);

            ViewData["AssetCount"] = count;
            ViewData["AssetTotalValue"] = totalValue;
            ViewData["ProfitLoss"] = profitLoss;
            ViewData["ROI"] = ROI;

            return View(model);
        }

        // GET: Moment/Create
        public ActionResult Create()
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            var model = _momentService.SelectPlayers(userId);
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

            model.OwnerId = Guid.Parse(User.Identity.GetUserId());

            if (_momentService.CreateMoment(model))
            {
                TempData["SaveResult"] = "Asset successfully added!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Asset could not be added. Please make sure that all required input fields are populated.");

            return View(model);
        }

        // GET: Moment/Details/{id}
        public ActionResult Details(int id)
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            var model = _momentService.GetMomentDetails(id, userId);

            return View(model);
        }

        // GET: Moment/Edit/{id}
        public ActionResult Edit(int id)
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            var detail = _momentService.GetMomentDetails(id, userId);
            var playerList = _momentService.SelectPlayers(userId);

            ViewBag.PlayerList = playerList;

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
                    PackPrice = detail.PackPrice,
                    IndividualMomentPrice = detail.IndividualMomentPrice
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

            model.OwnerId = Guid.Parse(User.Identity.GetUserId());
            var userId = model.OwnerId;

            if (_momentService.EditMoment(model, userId))
            {
                TempData["SaveResult"] = "Asset successfully modified!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Asset could not be modified. Please make sure that all required input fields are populated.");

            return View(model);
        }

        // GET: Moment/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            var model = _momentService.GetMomentDetails(id, userId);
            return View(model);
        }

        //POST: Moment/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult MomentDelete(int id)
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            _momentService.DeleteMoment(id, userId);

            TempData["SaveResult"] = "Asset successfully removed!";

            return RedirectToAction("Index");
        }

        // Helper Method
        //private MomentService CreateMomentService()
        //{
        //    var userId = Guid.Parse(User.Identity.GetUserId());
        //    var service = new MomentService(userId);
        //    return service;
        //}
    }
}