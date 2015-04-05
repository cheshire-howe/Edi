using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Dal.Abstract;
using Edi.Dal.Interfaces;
using Edi.Models.TradingPartners;

namespace Edi.Dal.Concrete
{
    public class VendorRepository : GenericRepository<Vendor>, IVendorRepository
    {
        private readonly PartnershipContext _dbContext;

        public VendorRepository(DbContext context)
            : base(context)
        {
            _dbContext = (_dbContext ?? (PartnershipContext)context);
        }

        public Vendor GetById(int id)
        {
            return _dbSet.FirstOrDefault(x => x.ID == id);
        }

        public async Task<Vendor> GetByIdAsync(int id)
        {
            return await _dbSet
                .FirstOrDefaultAsync(x => x.ID == id);
        }
    }
}
