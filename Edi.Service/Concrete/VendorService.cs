using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Dal.Interfaces;
using Edi.Models.TradingPartners;
using Edi.Service.Interfaces;

namespace Edi.Service.Concrete
{
    public class VendorService : IVendorService
    {
        private readonly IUnitOfWork<PartnershipContext> _unitOfWork;

        public VendorService(IUnitOfWork<PartnershipContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(Vendor entity)
        {
            _unitOfWork.VendorRepository.Add(entity);
            _unitOfWork.Commit();
        }

        public IEnumerable<Vendor> GetAll()
        {
            return _unitOfWork.VendorRepository.GetAll();
        }

        public Vendor GetById(int id)
        {
            return _unitOfWork.VendorRepository.GetById(id);
        }

        public void Update(Vendor entity)
        {
            _unitOfWork.VendorRepository.Edit(entity);
            _unitOfWork.Commit();
        }

        public void Delete(Vendor entity)
        {
            _unitOfWork.VendorRepository.Delete(entity);
            _unitOfWork.Commit();
        }
    }
}
