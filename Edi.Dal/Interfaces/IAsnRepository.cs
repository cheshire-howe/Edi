using Edi.Models.AsnModels;

namespace Edi.Dal.Interfaces
{
    public interface IAsnRepository : IGenericRepository<Asn>
    {
        Asn GetById(int id);
    }
}