using System.Threading.Tasks;
using Edi.Models.TradingPartners;

namespace Edi.Dal.Interfaces
{
    public interface IPartnershipRepository : IGenericRepository<Partnership>
    {
        Partnership GetById(int id);
        Task<Partnership> GetByIdAsync(int id);
        string GetUserId(string customerId, string vendorId);
    }
}