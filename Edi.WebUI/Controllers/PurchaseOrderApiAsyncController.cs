using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Edi.Models.PurchaseOrderModels;
using Edi.Service.Interfaces;

namespace Edi.WebUI.Controllers
{
    [Authorize(Roles = "User")]
    public class PurchaseOrderApiAsyncController : ApiController
    {
        private readonly IPurchaseOrderService _purchaseOrderService;

        public PurchaseOrderApiAsyncController(IPurchaseOrderService purchaseOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
        }

        // GET: api/PurchaseOrderApiAsync
        public IEnumerable<PurchaseOrder> GetPurchaseOrders()
        {
            return _purchaseOrderService.GetAll();
        }

        // GET: api/PurchaseOrderApiAsync/5
        [ResponseType(typeof(PurchaseOrder))]
        public async Task<IHttpActionResult> GetPurchaseOrder(int id)
        {
            //PurchaseOrder purchaseOrder = await db.PurchaseOrders.FindAsync(id);
            PurchaseOrder purchaseOrder = await _purchaseOrderService.GetByIdAsync(id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return Ok(purchaseOrder);
        }

        // PUT: api/PurchaseOrderApiAsync/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPurchaseOrder(int id, PurchaseOrder purchaseOrder)
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
                await _purchaseOrderService.UpdateAsync(purchaseOrder);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseOrderExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PurchaseOrderApiAsync
        [ResponseType(typeof(PurchaseOrder))]
        public async Task<IHttpActionResult> PostPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _purchaseOrderService.CreateAsync(purchaseOrder);

            return CreatedAtRoute("DefaultApi", new { id = purchaseOrder.ID }, purchaseOrder);
        }

        // DELETE: api/PurchaseOrderApiAsync/5
        [ResponseType(typeof(PurchaseOrder))]
        public async Task<IHttpActionResult> DeletePurchaseOrder(int id)
        {
            PurchaseOrder purchaseOrder = await _purchaseOrderService.GetByIdAsync(id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            await _purchaseOrderService.DeleteAsync(purchaseOrder);

            return Ok(purchaseOrder);
        }

        private bool PurchaseOrderExists(int id)
        {
            return _purchaseOrderService.GetAll().Count(e => e.ID == id) > 0;
        }
    }
}