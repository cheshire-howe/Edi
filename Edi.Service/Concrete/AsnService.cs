using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Dal.Interfaces;
using Edi.Logic.Interfaces;
using Edi.Models.AsnModels;
using Edi.Service.Interfaces;
using OopFactory.X12.Parsing.Model;

namespace Edi.Service.Concrete
{
    public class AsnService : IAsnService
    {
        private readonly IUnitOfWork<AsnContext> _unitOfWork;
        private IAsnLogic _asnLogic;

        public AsnService(IUnitOfWork<AsnContext> unitOfWork, IAsnLogic asnLogic)
        {
            _unitOfWork = unitOfWork;
            _asnLogic = asnLogic;
        }

        public void SaveAsnEdiFile(List<Interchange> interchanges, string userId)
        {
            var asn = _asnLogic.ConvertAsn(interchanges, userId);
            Create(asn);
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

        public IEnumerable<Asn> GetByUserId(string id)
        {
            return _unitOfWork.AsnRepository.FindBy(x => x.UserID == id);
        }

        public Asn GetById(int id)
        {
            return _unitOfWork.AsnRepository.GetById(id);
        }

        public async Task<Asn> GetByIdAsync(int id)
        {
            return await _unitOfWork.AsnRepository.GetByIdAsync(id);
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

        public async Task CreateAsync(Asn entity)
        {
            _unitOfWork.AsnRepository.Add(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Asn entity)
        {
            _unitOfWork.AsnRepository.Edit(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Asn entity)
        {
            _unitOfWork.AsnRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
        }
    }
}
