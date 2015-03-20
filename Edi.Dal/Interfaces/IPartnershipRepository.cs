using Edi.Models.TradingPartners;

namespace Edi.Dal.Interfaces
{
    public interface IPartnershipRepository : IGenericRepository<Partnership>
    {
        Partnership GetById(int id);
    }
}