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

        public async Task<Vendor> GetByIdAsync(int id)
        {
            return await _unitOfWork.VendorRepository.GetByIdAsync(id);
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

        public async Task CreateAsync(Vendor entity)
        {
            _unitOfWork.VendorRepository.Add(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Vendor entity)
        {
            _unitOfWork.VendorRepository.Edit(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Vendor entity)
        {
            _unitOfWork.VendorRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
        }
    }
}
