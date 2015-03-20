using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Edi.Dal.Interfaces;

namespace Edi.Dal.Concrete
{
    /// <summary>
    /// The Entity Framework implementation of IUnitOfWork
    /// </summary>
    public sealed class UnitOfWork<TContext> : IUnitOfWork<TContext>
        where TContext : DbContext, new()
    {
        /// <summary>
        /// The DbContext
        /// </summary>
        private DbContext _dbContext;

        /// <summary>
        /// Initializes a new UnitOfWork class
        /// </summary>
        /// <param name="context">The context</param>
        public UnitOfWork()
        {
            _dbContext = new TContext();
        }

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified or Deleted state</returns>
        public int Commit()
        {
            try
            {
                return _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            // Save changes with the default options
        }

        #region Repositories

        /// <summary>
        /// Invoice Repository
        /// </summary>
        private IInvoiceRepository _invoiceRepository;
        public IInvoiceRepository InvoiceRepository
        {
            get { return _invoiceRepository ?? (_invoiceRepository = new InvoiceRepository(_dbContext)); }
        }

        /// <summary>
        /// Purchase Order Repository
        /// </summary>
        private IPurchaseOrderRepository _purchaseOrderRepository;
        public IPurchaseOrderRepository PurchaseOrderRepository
        {
            get { return _purchaseOrderRepository ?? (_purchaseOrderRepository = new PurchaseOrderRepository(_dbContext)); }
        }

        private IAcknowledgmentRepository _acknowledgmentRepository;

        public IAcknowledgmentRepository AcknowledgmentRepository
        {
            get
            {
                return _acknowledgmentRepository ??
                       (_acknowledgmentRepository = new AcknowledgmentRepository(_dbContext));
            }
        }

        private IAsnRepository _asnRepository;

        public IAsnRepository AsnRepository
        {
            get { return _asnRepository ?? (_asnRepository = new AsnRepository(_dbContext)); }
        }

        /// <summary>
        /// Vendor Repository
        /// </summary>
        private IVendorRepository _vendorRepository;
        public IVendorRepository VendorRepository
        {
            get { return _vendorRepository ?? (_vendorRepository = new VendorRepository(_dbContext)); }
        }

        /// <summary>
        /// Partnership Repository
        /// </summary>
        private IPartnershipRepository _partnershipRepository;
        public IPartnershipRepository PartnershipRepository
        {
            get { return _partnershipRepository ?? (_partnershipRepository = new PartnershipRepository(_dbContext)); }
        }

        #endregion

        /// <summary>
        /// Disposes the current object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes all external resources
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }
        }
    }
}
