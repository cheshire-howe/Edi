using Edi.Models.TradingPartners;

namespace Edi.Dal.Interfaces
{
    public interface IVendorRepository : IGenericRepository<Vendor>
    {
        Vendor GetById(int id);
    }
}