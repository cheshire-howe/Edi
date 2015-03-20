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
    public class PartnershipRepository : GenericRepository<Partnership>, IPartnershipRepository
    {
        private readonly PartnershipContext _dbContext;

        public PartnershipRepository(DbContext context)
            : base(context)
        {
            _dbContext = (_dbContext ?? (PartnershipContext)context);
        }

        public Partnership GetById(int id)
        {
            return _dbSet.FirstOrDefault(x => x.ID == id);
        }

        public string GetUserId(string customerId, string vendorId)
        {
            var userId = _dbSet.FirstOrDefault(x => x.CustomerEdiID == customerId && x.VendorEdiID == vendorId);
            return userId != null ? userId.UserID : "";
        }
    }
}
