using System.Web.Mvc;

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

        public ActionResult PoCreate()
        {
            return PartialView("PoPartials/Create");
        }

        public ActionResult PoLineItem()
        {
            return PartialView("PoPartials/FormPartials/LineItem");
        }

        public ActionResult PoDtm()
        {
            ViewBag.MyVar = "Cool stuff";
            return PartialView("PoPartials/FormPartials/Dtm");
        }

        public ActionResult PoItemDtm()
        {
            return PartialView("PoPartials/FormPartials/ItemDtm");
        }
    }
}
