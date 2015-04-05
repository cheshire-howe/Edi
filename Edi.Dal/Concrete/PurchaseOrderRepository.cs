using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Dal.Abstract;
using Edi.Dal.Interfaces;
using Edi.Models.PurchaseOrderModels;

namespace Edi.Dal.Concrete
{
    public class PurchaseOrderRepository : GenericRepository<PurchaseOrder>, IPurchaseOrderRepository
    {
        private readonly PurchaseOrderContext _dbContext;

        public PurchaseOrderRepository(DbContext context) : base(context)
        {
            _dbContext = (_dbContext ?? (PurchaseOrderContext) context);
        }

        public PurchaseOrder GetById(int id)
        {
            return _dbSet
                .Include(x => x.Dtms)
                .Include(x => x.Items)
                .Include(x => x.Names)
                .Include(x => x.Refs)
                .FirstOrDefault(x => x.ID == id);
        }

        public async Task<PurchaseOrder> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(x => x.Dtms)
                .Include(x => x.Items)
                .Include(x => x.Names)
                .Include(x => x.Refs)
                .FirstOrDefaultAsync(x => x.ID == id);
        } 
    }
}
