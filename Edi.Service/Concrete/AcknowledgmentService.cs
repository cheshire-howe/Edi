using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Dal.Interfaces;
using Edi.Models.AcknowledgmentModels;
using Edi.Service.Interfaces;

namespace Edi.Service.Concrete
{
    public class AcknowledgmentService : IAcknowledgmentService
    {
        private readonly IUnitOfWork<AcknowledgmentContext> _unitOfWork;

        public AcknowledgmentService(IUnitOfWork<AcknowledgmentContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Create(Acknowledgment entity)
        {
            _unitOfWork.AcknowledgmentRepository.Add(entity);
            _unitOfWork.Commit();
        }

        public IEnumerable<Acknowledgment> GetAll()
        {
            return _unitOfWork.AcknowledgmentRepository.GetAll();
        }

        public Acknowledgment GetById(int id)
        {
            return _unitOfWork.AcknowledgmentRepository.GetById(id);
        }

        public void Update(Acknowledgment entity)
        {
            _unitOfWork.AcknowledgmentRepository.Edit(entity);
            _unitOfWork.Commit();
        }

        public void Delete(Acknowledgment entity)
        {
            _unitOfWork.AcknowledgmentRepository.Delete(entity);
            _unitOfWork.Commit();
        }
    }
}
