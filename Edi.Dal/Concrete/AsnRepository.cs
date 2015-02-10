using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Dal.Abstract;
using Edi.Dal.Interfaces;
using Edi.Models.AsnModels;

namespace Edi.Dal.Concrete
{
    public class AsnRepository : GenericRepository<Asn>, IAsnRepository
    {
        private readonly AsnContext _dbContext;

        public AsnRepository(DbContext context) : base(context)
        {
            _dbContext = (_dbContext ?? (AsnContext) context);
        }

        public Asn GetById(int id)
        {
            return _dbSet.FirstOrDefault(x => x.ID == id);
        }
    }
}
