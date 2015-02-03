using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Dal.Interfaces;
using Edi.Models.PurchaseOrderModels;
using Edi.Service.Interfaces;

namespace Edi.Service.Concrete
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IUnitOfWork<PurchaseOrderContext> _unitOfWork;

        public PurchaseOrderService(IUnitOfWork<PurchaseOrderContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(PurchaseOrder entity)
        {
            _unitOfWork.PurchaseOrderRepository.Add(entity);
            _unitOfWork.Commit();
        }

        public IEnumerable<PurchaseOrder> GetAll()
        {
            return _unitOfWork.PurchaseOrderRepository.GetAll();
        }

        public void Update(PurchaseOrder entity)
        {
            _unitOfWork.PurchaseOrderRepository.Edit(entity);
            _unitOfWork.Commit();
        }

        public void Delete(PurchaseOrder entity)
        {
            _unitOfWork.PurchaseOrderRepository.Delete(entity);
            _unitOfWork.Commit();
        }
    }
}
