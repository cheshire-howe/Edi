using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Dal.Abstract;
using Edi.Dal.Interfaces;
using Edi.Models;
using Edi.Models.InvoiceModels;

namespace Edi.Dal.Concrete
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        private readonly InvoiceContext _dbContext;

        public InvoiceRepository(DbContext context) : base(context)
        {
            _dbContext = (_dbContext ?? (InvoiceContext) context);
        }

        public Invoice GetById(int id)
        {
            return _dbSet
                .Include(x => x.Names)
                .Include(x => x.Notes)
                .Include(x => x.Items)
                .Include(x => x.Refs)
                .FirstOrDefault(x => x.ID == id);
        }

        public async Task<Invoice> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(x => x.Names)
                .Include(x => x.Notes)
                .Include(x => x.Items)
                .Include(x => x.Refs)
                .FirstOrDefaultAsync(x => x.ID == id);
        }

        public override IEnumerable<Invoice> GetAll()
        {
            return _dbSet
                .Include(x => x.Names)
                .Include(x => x.Notes)
                .Include(x => x.Items)
                .Include(x => x.Refs)
                .AsEnumerable();
        }
    }
}
