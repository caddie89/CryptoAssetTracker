using CAT.Services.Moment_Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CAT.WebMVC.Controllers.Moment
{
    public class MomentController : Controller
    {
        // GET: Moment/Index
        public ActionResult Index()
        {
            var service = CreateMomentService();
            var model = service.GetMomentIndex();
            return View();
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