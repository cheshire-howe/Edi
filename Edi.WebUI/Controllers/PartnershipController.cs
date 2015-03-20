using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Edi.Models.TradingPartners;
using Edi.Service.Interfaces;
using Edi.WebUI.Models;

namespace Edi.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PartnershipController : Controller
    {
        private readonly ApplicationDbContext _userContext = new ApplicationDbContext();
        private readonly IPartnershipService _partnershipService;
        private readonly IVendorService _vendorService;

        public PartnershipController(IPartnershipService partnershipService,
            IVendorService vendorService)
        {
            _partnershipService = partnershipService;
            _vendorService = vendorService;
        }

        // GET: Partnership
        public ActionResult Index()
        {
            var partnerships = _partnershipService.GetAll();
            return View(partnerships.ToList());
        }

        // GET: Partnership/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partnership partnership = _partnershipService.GetById(id.Value);
            if (partnership == null)
            {
                return HttpNotFound();
            }
            return View(partnership);
        }

        // GET: Partnership/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(_userContext.Users, "Id", "UserName");
            ViewBag.VendorID = new SelectList(_vendorService.GetAll(), "ID", "Name");
            return View();
        }

        // POST: Partnership/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,VendorID,CustomerIDQualifier,CustomerEdiID,VendorIDQualifier,VendorEdiID")] Partnership partnership)
        {
            if (ModelState.IsValid)
            {
                _partnershipService.Create(partnership);
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(_userContext.Users, "Id", "UserName");
            ViewBag.VendorID = new SelectList(_vendorService.GetAll(), "ID", "Name", partnership.VendorID);
            return View(partnership);
        }

        // GET: Partnership/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partnership partnership = _partnershipService.GetById(id.Value);
            if (partnership == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(_userContext.Users, "Id", "UserName", partnership.UserID);
            ViewBag.VendorID = new SelectList(_vendorService.GetAll(), "ID", "Name", partnership.VendorID);
            return View(partnership);
        }

        // POST: Partnership/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,VendorID,CustomerIDQualifier,CustomerEdiID,VendorIDQualifier,VendorEdiID")] Partnership partnership)
        {
            if (ModelState.IsValid)
            {
                _partnershipService.Update(partnership);
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(_userContext.Users, "Id", "UserName", partnership.UserID);
            ViewBag.VendorID = new SelectList(_vendorService.GetAll(), "ID", "Name", partnership.VendorID);
            return View(partnership);
        }

        // GET: Partnership/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partnership partnership = _partnershipService.GetById(id.Value);
            if (partnership == null)
            {
                return HttpNotFound();
            }
            return View(partnership);
        }

        // POST: Partnership/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Partnership partnership = _partnershipService.GetById(id);
            _partnershipService.Delete(partnership);
            return RedirectToAction("Index");
        }
    }
}
