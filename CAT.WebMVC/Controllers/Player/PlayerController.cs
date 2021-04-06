using CAT.Models.Player_Models;
using CAT.Services.Player_Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CAT.WebMVC.Controllers.Player
{
    [Authorize]
    public class PlayerController : Controller
    {
        // GET: Player/Create
        public ActionResult Create()
        {
            return View();
        }

        // PUT: Player/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlayerCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var service = CreatePlayerService();

            if (service.CreatePlayer(model))
            {
                TempData["SaveResult"] = "Player successfully added.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Player could not be created. Please make sure that all input field are populated.");

            return View(model);
        }

        // GET: Player/Index
        public ActionResult Index()
        {
            var service = CreatePlayerService();
            var model = service.GetAllPlayers();

            return View(model);
        }

        // Helper Method - CreateService()
        private PlayerService CreatePlayerService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());

            var service = new PlayerService(userId);
            return service;
        }
    }
}