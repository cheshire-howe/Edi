using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Dal.Interfaces;
using Edi.Logic.Interfaces;
using Edi.Models.InvoiceModels;
using Edi.Service.Interfaces;
using OopFactory.X12.Parsing.Model;

namespace Edi.Service.Concrete
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork<InvoiceContext> _unitOfWork;
        private readonly IInvoiceLogic _invoiceLogic;

        public InvoiceService(IUnitOfWork<InvoiceContext> unitOfWork, IInvoiceLogic invoiceLogic)
        {
            _unitOfWork = unitOfWork;
            _invoiceLogic = invoiceLogic;
        }

        public void SaveEdiFile(List<Interchange> interchanges, string userId)
        {
            var invoice = _invoiceLogic.ConvertInvoice(interchanges, userId);
            Create(invoice);
        }

        public void WriteEdiFile(int id)
        {
            var invoice = _unitOfWork.InvoiceRepository.GetById(id);
            _invoiceLogic.WriteInvoiceEdi(invoice);
        }

        public void Create(Invoice entity)
        {
            _unitOfWork.InvoiceRepository.Add(entity);
            _unitOfWork.Commit();
        }

        public IEnumerable<Invoice> GetAll()
        {
            return _unitOfWork.InvoiceRepository.GetAll();
        }

        public IEnumerable<Invoice> GetByUserId(string id)
        {
            return _unitOfWork.InvoiceRepository.FindBy(x => x.UserID == id);
        }

        public Invoice GetById(int id)
        {
            return _unitOfWork.InvoiceRepository.GetById(id);
        }

        public void Update(Invoice entity)
        {
            _unitOfWork.InvoiceRepository.Edit(entity);
            _unitOfWork.Commit();
        }

        public void Delete(Invoice entity)
        {
            _unitOfWork.InvoiceRepository.Delete(entity);
            _unitOfWork.Commit();
        }
    }
}
