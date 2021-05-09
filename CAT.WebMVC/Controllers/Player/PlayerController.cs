using CAT.Contracts.Player_Contract;
using CAT.Models.Player_Models;
using CAT.Services.Player_Service;
using Microsoft.AspNet.Identity;
using PagedList;
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
        private Guid _userId;

        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        //GET: Player/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST: Player/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlayerCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.OwnerId = Guid.Parse(User.Identity.GetUserId());

            if (_playerService.CreatePlayer(model))
            {
                TempData["SaveResult"] = "Player successfully added!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Player could not be created. Please make sure that all input field are populated.");

            return View(model);
        }

        //GET: Player/Index
        public ActionResult Index(string search, int? page)
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            var model = _playerService.GetPlayerIndex(search, page, userId);
            return View(model);
        }

        //GET: Player/Detail/{id}
        public ActionResult Details(int id)
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            var model = _playerService.GetPlayerDetails(id, userId);

            return View(model);
        }

        //GET: Player/Edit/{id}
        public ActionResult Edit(int id)
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            var detail = _playerService.GetPlayerDetails(id, userId);
            var model =
                new PlayerEdit
                {
                    PlayerId = detail.PlayerId,
                    PlayerFirstName = detail.PlayerFirstName,
                    PlayerLastName = detail.PlayerLastName,
                    PositionOfPlayer = detail.PositionOfPlayer,
                    PlayerTeam = detail.PlayerTeam
                };
            return View(model);
        }

        //POST: Player/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PlayerEdit model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.PlayerId != id)
                ModelState.AddModelError("", "IDs do not match.");

            model.OwnerId = Guid.Parse(User.Identity.GetUserId());
            var userId = model.OwnerId;

            if (_playerService.EditPlayer(model, userId))
            {
                TempData["SaveResult"] = "Player successfully modified!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Player could not be updated. Please make sure that all required input fields are populated.");
            return View(model);
        }

        //GET: Player/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            var model = _playerService.GetPlayerDetails(id, userId);
            return View(model);
        }

        //POST: Player/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            _userId = Guid.Parse(User.Identity.GetUserId());
            var userId = _userId;

            _playerService.DeletePlayer(id, userId);

            TempData["SaveResult"] = "Player successfully removed!";

            return RedirectToAction("Index");
        }
    }
}