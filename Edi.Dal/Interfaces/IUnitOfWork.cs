using System;

namespace Edi.Dal.Interfaces
{
    public interface IUnitOfWork<TContext> : IDisposable
    {
        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified or Deleted state</returns>
        int Commit();

        /// <summary>
        /// Invoice Repository
        /// </summary>
        IInvoiceRepository InvoiceRepository { get; }
        IPurchaseOrderRepository PurchaseOrderRepository { get; }
        IAcknowledgmentRepository AcknowledgmentRepository { get; }
        IAsnRepository AsnRepository { get; }
    }
}