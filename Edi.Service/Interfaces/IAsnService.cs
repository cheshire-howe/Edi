﻿using System.Collections.Generic;
using System.IO;
using Edi.Models.AsnModels;
using OopFactory.X12.Parsing.Model;

namespace Edi.Service.Interfaces
{
    public interface IAsnService : IEntityService<Asn>
    {
        Asn GetById(int id);
        IEnumerable<Asn> GetByUserId(string id);
        void SaveAsnEdiFile(List<Interchange> interchanges, string userId);
    }
}