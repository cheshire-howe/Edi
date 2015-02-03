using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Edi.Models.InvoiceModels;
using Edi.Service.Interfaces;
using Edi.WebUI.Models;

namespace Edi.WebUI.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        
        // GET: Invoices
        public ActionResult Index()
        {
            return View(_invoiceService.GetAll().ToList());
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = _invoiceService.GetById(id.Value);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CustomerID,BIG01_Date,BIG02_InvoiceNumber,BIG03_Date,BIG04_PurchaseOrderNumber,BIG04_TransactionTypeCode,BIG08_TransactionSetPurposeCode,CUR01_CurrencyEntityIdentifierCode,CUR02_CurrencyCode,CUR03_ExchangeRate,ITD01_TermsTypeCode,ITD02_TermsBasisDateCode,ITD07_TermsNetDays,ITD12_Description,DTM01_DateTimeQualifier,DTM02_ShipDate,TDS01_Amount,CTT01_TransactionTotals")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _invoiceService.Create(invoice);
                return RedirectToAction("Index");
            }

            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = _invoiceService.GetById(id.Value);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CustomerID,BIG01_Date,BIG02_InvoiceNumber,BIG03_Date,BIG04_PurchaseOrderNumber,BIG04_TransactionTypeCode,BIG08_TransactionSetPurposeCode,CUR01_CurrencyEntityIdentifierCode,CUR02_CurrencyCode,CUR03_ExchangeRate,ITD01_TermsTypeCode,ITD02_TermsBasisDateCode,ITD07_TermsNetDays,ITD12_Description,DTM01_DateTimeQualifier,DTM02_ShipDate,TDS01_Amount,CTT01_TransactionTotals")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _invoiceService.Update(invoice);
                return RedirectToAction("Index");
            }
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = _invoiceService.GetById(id.Value);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = _invoiceService.GetById(id);
            _invoiceService.Delete(invoice);
            return RedirectToAction("Index");
        }
    }
}
