using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Dal.Interfaces;
using Edi.Models.AsnModels;
using Edi.Service.Interfaces;

namespace Edi.Service.Concrete
{
    public class AsnService : IAsnService
    {
        private readonly IUnitOfWork<AsnContext> _unitOfWork;

        public AsnService(IUnitOfWork<AsnContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(Asn entity)
        {
            _unitOfWork.AsnRepository.Add(entity);
            _unitOfWork.Commit();
        }

        public IEnumerable<Asn> GetAll()
        {
            return _unitOfWork.AsnRepository.GetAll();
        }

        public Asn GetById(int id)
        {
            return _unitOfWork.AsnRepository.GetById(id);
        }

        public void Update(Asn entity)
        {
            _unitOfWork.AsnRepository.Edit(entity);
            _unitOfWork.Commit();
        }

        public void Delete(Asn entity)
        {
            _unitOfWork.AsnRepository.Delete(entity);
            _unitOfWork.Commit();
        }
    }
}
