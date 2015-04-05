using System.Threading.Tasks;
using Edi.Models.AsnModels;

namespace Edi.Dal.Interfaces
{
    public interface IAsnRepository : IGenericRepository<Asn>
    {
        Asn GetById(int id);
        Task<Asn> GetByIdAsync(int id);
    }
}