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
    public class PurchaseOrderController : Controller
    {
        private readonly IPurchaseOrderService _purchaseOrderService;

        public PurchaseOrderController(IPurchaseOrderService purchaseOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
        }

        // GET: PurchaseOrder
        public ActionResult Index()
        {
            return View(_purchaseOrderService.GetAll().ToList());
        }

        // GET: PurchaseOrder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = _purchaseOrderService.GetById(id.Value);
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrder);
        }

        // GET: PurchaseOrder/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PurchaseOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CustomerID,BEG01_TransactionSetPurposeCode,BEG02_PurchaseOrderTypeCode,BEG03_PurchaseOrderNumber,BEG05_Date,CUR01_CurrencyEntityIdentifierCode,CUR02_CurrencyCode,CTT01_NumberofLineItems,AMT01_AmountQualifierCode,AMT02_Amount")] PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                _purchaseOrderService.SaveAndSend(purchaseOrder, 1);
                return RedirectToAction("Index");
            }

            return View(purchaseOrder);
        }

        // GET: PurchaseOrder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = _purchaseOrderService.GetById(id.Value);
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrder);
        }

        // POST: PurchaseOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CustomerID,BEG01_TransactionSetPurposeCode,BEG02_PurchaseOrderTypeCode,BEG03_PurchaseOrderNumber,BEG05_Date,CUR01_CurrencyEntityIdentifierCode,CUR02_CurrencyCode,CTT01_NumberofLineItems,AMT01_AmountQualifierCode,AMT02_Amount")] PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                _purchaseOrderService.Update(purchaseOrder);
                return RedirectToAction("Index");
            }
            return View(purchaseOrder);
        }

        // GET: PurchaseOrder/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseOrder purchaseOrder = _purchaseOrderService.GetById(id.Value);
            if (purchaseOrder == null)
            {
                return HttpNotFound();
            }
            return View(purchaseOrder);
        }

        // POST: PurchaseOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseOrder purchaseOrder = _purchaseOrderService.GetById(id);
            _purchaseOrderService.Delete(purchaseOrder);
            return RedirectToAction("Index");
        }
    }
}
