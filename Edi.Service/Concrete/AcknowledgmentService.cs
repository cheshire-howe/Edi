﻿using System;
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

        public void SaveACKEdiFile(List<Interchange> interchanges, string userId)
        {
            var acknowledgment = _acknowledgmentLogic.ConvertAcknowledgment(interchanges, userId);
            Create(acknowledgment);
        }

        public IEnumerable<Acknowledgment> GetAll()
        {
            return _unitOfWork.AcknowledgmentRepository.GetAll();
        }

        public IEnumerable<Acknowledgment> GetByUserId(string id)
        {
            return _unitOfWork.AcknowledgmentRepository.FindBy(x => x.UserID == id);
        } 

        public Acknowledgment GetById(int id)
        {
            return _unitOfWork.AcknowledgmentRepository.GetById(id);
        }

        public async Task<Acknowledgment> GetByIdAsync(int id)
        {
            return await _unitOfWork.AcknowledgmentRepository.GetByIdAsync(id);
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


        public async Task CreateAsync(Acknowledgment entity)
        {
            _unitOfWork.AcknowledgmentRepository.Add(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Acknowledgment entity)
        {
            _unitOfWork.AcknowledgmentRepository.Edit(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Acknowledgment entity)
        {
            _unitOfWork.AcknowledgmentRepository.Delete(entity);
            await _unitOfWork.CommitAsync();
        }
    }
}
