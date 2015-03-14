using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Edi.Models.PurchaseOrderModels;
using Edi.Service.Interfaces;

namespace Edi.WebUI.Controllers
{
    public class PurchaseOrderApiController : ApiController
    {
        private readonly IPurchaseOrderService _purchaseOrderService;

        public PurchaseOrderApiController(IPurchaseOrderService purchaseOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
        }

        // GET: api/PurchaseOrderApi
        public IEnumerable<PurchaseOrder> GetPurchaseOrders()
        {
            return _purchaseOrderService.GetAll().ToList();
        }

        // GET: api/PurchaseOrderApi/5
        [ResponseType(typeof(PurchaseOrder))]
        public IHttpActionResult GetPurchaseOrder(int id)
        {
            PurchaseOrder purchaseOrder = _purchaseOrderService.GetById(id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return Ok(purchaseOrder);
        }

        // PUT: api/PurchaseOrderApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchaseOrder(int id, PurchaseOrder purchaseOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchaseOrder.ID)
            {
                return BadRequest();
            }

            try
            {
                _purchaseOrderService.Update(purchaseOrder);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseOrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PurchaseOrderApi
        [ResponseType(typeof(PurchaseOrder))]
        public IHttpActionResult PostPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _purchaseOrderService.Create(purchaseOrder);

            return CreatedAtRoute("DefaultApi", new { id = purchaseOrder.ID }, purchaseOrder);
        }

        // DELETE: api/PurchaseOrderApi/5
        [ResponseType(typeof(PurchaseOrder))]
        public IHttpActionResult DeletePurchaseOrder(int id)
        {
            PurchaseOrder purchaseOrder = _purchaseOrderService.GetById(id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            _purchaseOrderService.Delete(purchaseOrder);

            return Ok(purchaseOrder);
        }

        private bool PurchaseOrderExists(int id)
        {
            return _purchaseOrderService.GetAll().Count(e => e.ID == id) > 0;
        }
    }
}