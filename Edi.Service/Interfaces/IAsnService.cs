using Edi.Models.AsnModels;

namespace Edi.Service.Interfaces
{
    public interface IAsnService : IEntityService<Asn>
    {
        Asn GetById(int id);
    }
}