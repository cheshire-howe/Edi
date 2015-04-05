using System.Collections.Generic;
using System.Threading.Tasks;
using Edi.Models.TradingPartners;

namespace Edi.Service.Interfaces
{
    public interface IVendorService : IEntityService<Vendor>
    {
        Vendor GetById(int id);
        Task<Vendor> GetByIdAsync(int id);
    }
}