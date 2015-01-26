using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Dal.Interfaces;
using Edi.Models.PurchaseOrderModels;

namespace Edi.Logic
{
    public class PurchaseOrderLogic
    {
        private readonly IUnitOfWork<PurchaseOrderContext> _unitOfWork;

        public PurchaseOrderLogic(IUnitOfWork<PurchaseOrderContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<PurchaseOrder> GetPurchaseOrders()
        {
            return _unitOfWork.PurchaseOrderRepository.GetAll().ToList();
        } 
    }
}
