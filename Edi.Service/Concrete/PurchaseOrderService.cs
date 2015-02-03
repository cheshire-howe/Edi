using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Dal.Interfaces;
using Edi.Models.PurchaseOrderModels;
using Edi.Service.Interfaces;
using System.IO;
using Edi.Logic.Interfaces;

namespace Edi.Service.Concrete
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IUnitOfWork<PurchaseOrderContext> _unitOfWork;
        private readonly IPurchaseOrderLogic _purchaseOrderLogic;

        public PurchaseOrderService(IUnitOfWork<PurchaseOrderContext> unitOfWork, IPurchaseOrderLogic purchaseOrderLogic)
        {
            _unitOfWork = unitOfWork;
            _purchaseOrderLogic = purchaseOrderLogic;
        }

        public void SavePOEdiFile(FileStream fs)
        {
            var purchaseOrder = _purchaseOrderLogic.ConvertPurchaseOrder(fs);
            Create(purchaseOrder);
        }

        public void WritePOEdiFile(int id)
        {
            var purchaseOrder = _unitOfWork.PurchaseOrderRepository.GetById(id);
            _purchaseOrderLogic.WritePurchaseOrderEdi(purchaseOrder);
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

        public PurchaseOrder GetById(int id)
        {
            return _unitOfWork.PurchaseOrderRepository.GetById(id);
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
