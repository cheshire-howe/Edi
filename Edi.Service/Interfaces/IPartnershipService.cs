using Edi.Models.TradingPartners;

namespace Edi.Service.Interfaces
{
    public interface IPartnershipService : IEntityService<Partnership>
    {
        Partnership GetById(int id);
    }
}