﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Dal.Interfaces;
using Edi.Models.PurchaseOrderModels;
using Edi.Service.Interfaces;
using System.IO;
using Edi.Logic.Interfaces;
using OopFactory.X12.Parsing.Model;

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

        public void SavePOEdiFile(List<Interchange> interchanges)
        {
            var purchaseOrder = _purchaseOrderLogic.ConvertPurchaseOrder(interchanges);
            Create(purchaseOrder);
        }

        public void SaveAndSend(PurchaseOrder entity, int customerId)
        {
            Create(entity);
            WritePOEdiFile(entity.ID, customerId);
        }

        public void WritePOEdiFile(int id, int customerId)
        {
            var purchaseOrder = _unitOfWork.PurchaseOrderRepository.GetById(id);
            var filename = _purchaseOrderLogic.WritePurchaseOrderEdi(purchaseOrder, customerId);
            _purchaseOrderLogic.SendPurchaseOrder(filename);
        }

        public void Create(PurchaseOrder entity)
        {
            entity.UserID = "41d00c91-1642-4a1b-b3d0-c07aea218c64";
            _unitOfWork.PurchaseOrderRepository.Add(entity);
            _unitOfWork.Commit();
        }

        public async Task CreateAsync(PurchaseOrder entity)
        {
            entity.UserID = "41d00c91-1642-4a1b-b3d0-c07aea218c64";
            _unitOfWork.PurchaseOrderRepository.Add(entity);
            await _unitOfWork.CommitAsync();
        }

        public IEnumerable<PurchaseOrder> GetAll()
        {
            return _unitOfWork.PurchaseOrderRepository.GetAll();
        }

        public IEnumerable<PurchaseOrder> GetByUserId(string id)
        {
            return _unitOfWork.PurchaseOrderRepository.FindBy(x => x.UserID == id);
        } 

        public PurchaseOrder GetById(int id)
        {
            return _unitOfWork.PurchaseOrderRepository.GetById(id);
        }

        public async Task<PurchaseOrder> GetByIdAsync(int id)
        {
            return await _unitOfWork.PurchaseOrderRepository.GetByIdAsync(id);
        }

        public void Update(PurchaseOrder entity)
        {
            _unitOfWork.PurchaseOrderRepository.Edit(entity);
            _unitOfWork.Commit();
        }

        public async Task UpdateAsync(PurchaseOrder entity)
        {
            _unitOfWork.PurchaseOrderRepository.Edit(entity);
            await _unitOfWork.CommitAsync();
        }

        public void Delete(PurchaseOrder entity)
        {
            _unitOfWork.PurchaseOrderRepository.Delete(entity);
            _unitOfWork.Commit();
        }

        public async Task DeleteAsync(PurchaseOrder entity)
        {
            _unitOfWork.PurchaseOrderRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
        }
    }
}
