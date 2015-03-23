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
using Microsoft.AspNet.Identity;

namespace Edi.WebUI.Controllers
{
    [Authorize(Roles = "User")]
    public class PurchaseOrderApiController : ApiController
    {
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IPartnershipService _partnershipService;

        public PurchaseOrderApiController(IPurchaseOrderService purchaseOrderService, IPartnershipService partnershipService)
        {
            _purchaseOrderService = purchaseOrderService;
            _partnershipService = partnershipService;
        }

        // GET: api/PurchaseOrderApi
        public IEnumerable<PurchaseOrder> GetPurchaseOrders()
        {
            string id = User.Identity.GetUserId();
            return _purchaseOrderService.GetByUserId(id).ToList();
        }

        // GET: api/PurchaseOrderApi/5
        [ResponseType(typeof(PurchaseOrder))]
        public IHttpActionResult GetPurchaseOrder(int id)
        {
            // TODO: Check if current user id == po user id
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

            // TODO: Partnership Api to load dropdown with envelope info
            purchaseOrder.PoEnvelope.ISA05_InterchangeSenderIdQualifier = "01";
            purchaseOrder.PoEnvelope.ISA06_InterchangeSenderId = "828513080      ";
            purchaseOrder.PoEnvelope.ISA07_InterchangeReceiverIdQualifier = "01";
            purchaseOrder.PoEnvelope.ISA08_InterchangeReceiverId = "001903202U     ";
            
            purchaseOrder.UserID = User.Identity.GetUserId();

            for (int i = 0; i < purchaseOrder.Items.Count; i++)
            {
                purchaseOrder.Items[i].PO101_AssignedIdentification = (i + 1).ToString();
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