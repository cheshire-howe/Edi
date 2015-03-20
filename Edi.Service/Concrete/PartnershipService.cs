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
    public class PartnershipService : IPartnershipService
    {
        private readonly IUnitOfWork<PartnershipContext> _unitOfWork;

        public PartnershipService(IUnitOfWork<PartnershipContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(Partnership entity)
        {
            _unitOfWork.PartnershipRepository.Add(entity);
            _unitOfWork.Commit();
        }

        public IEnumerable<Partnership> GetAll()
        {
            return _unitOfWork.PartnershipRepository.GetAll();
        }

        public Partnership GetById(int id)
        {
            return _unitOfWork.PartnershipRepository.GetById(id);
        }

        public string GetUserId(string customerId, string vendorId)
        {
            customerId = customerId.Trim();
            vendorId = vendorId.Trim();
            return _unitOfWork.PartnershipRepository.GetUserId(customerId, vendorId);
        }

        public void Update(Partnership entity)
        {
            _unitOfWork.PartnershipRepository.Edit(entity);
            _unitOfWork.Commit();
        }

        public void Delete(Partnership entity)
        {
            _unitOfWork.PartnershipRepository.Delete(entity);
            _unitOfWork.Commit();
        }
    }
}
