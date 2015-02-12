using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Dal.Interfaces;
using Edi.Logic.Interfaces;
using Edi.Models.AcknowledgmentModels;
using Edi.Service.Interfaces;
using OopFactory.X12.Parsing.Model;

namespace Edi.Service.Concrete
{
    public class AcknowledgmentService : IAcknowledgmentService
    {
        private readonly IUnitOfWork<AcknowledgmentContext> _unitOfWork;
        private readonly IAcknowledgmentLogic _acknowledgmentLogic;


        public AcknowledgmentService(IUnitOfWork<AcknowledgmentContext> unitOfWork, IAcknowledgmentLogic acknowledgmentLogic)
        {
            _unitOfWork = unitOfWork;
            _acknowledgmentLogic = acknowledgmentLogic;
        }

        public void Create(Acknowledgment entity)
        {
            _unitOfWork.AcknowledgmentRepository.Add(entity);
            _unitOfWork.Commit();
        }

        public void SaveACKEdiFile(List<Interchange> interchanges)
        {
            var acknowledgment = _acknowledgmentLogic.ConvertAcknowledgment(interchanges);
            Create(acknowledgment);
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
