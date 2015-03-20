using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Edi.Models.PurchaseOrderModels;
using Edi.Service.Interfaces;

namespace Edi.WebUI.Controllers
{
    [Authorize(Roles = "User")]
    public class PurchaseOrderController : Controller
    {
        /////////////////////////////////////////////////////////
        /// Purchase Orders with Templates
        /////////////////////////////////////////////////////////

        // GET: PurchaseOrder
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Po()
        {
            return PartialView("PoPartials/Po");
        }

        public ActionResult PoDetails()
        {
            return PartialView("PoPartials/Details");
        }
    }
}
