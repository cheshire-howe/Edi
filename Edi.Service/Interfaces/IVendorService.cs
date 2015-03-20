using System.Collections.Generic;
using Edi.Models.TradingPartners;

namespace Edi.Service.Interfaces
{
    public interface IVendorService : IEntityService<Vendor>
    {
        Vendor GetById(int id);
    }
}