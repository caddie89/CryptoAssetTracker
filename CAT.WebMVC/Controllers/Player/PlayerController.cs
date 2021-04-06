using CAT.Services.Player_Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CAT.WebMVC.Controllers.Player
{
    public class PlayerController : Controller
    {
        // GET: Player/Create

        // PUT: Player/Create

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