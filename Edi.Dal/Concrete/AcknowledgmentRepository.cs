using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edi.Dal.Abstract;
using Edi.Dal.Interfaces;
using Edi.Models.AcknowledgmentModels;

namespace Edi.Dal.Concrete
{
    public class AcknowledgmentRepository : GenericRepository<Acknowledgment>, IAcknowledgmentRepository
    {
        private readonly AcknowledgmentContext _dbContext;

        public AcknowledgmentRepository(DbContext context) : base(context)
        {
            _dbContext = (_dbContext ?? (AcknowledgmentContext) context);
        }

        public Acknowledgment GetById(int id)
        {
            return _dbSet
                .FirstOrDefault(x => x.ID == id);
        }

        public async Task<Acknowledgment> GetByIdAsync(int id)
        {
            return await _dbSet
                .FirstOrDefaultAsync(x => x.ID == id);
        }

        public override IEnumerable<Acknowledgment> GetAll()
        {
            return _dbSet
                .Include(x => x.AckNames)
                .Include(x => x.AckRefs)
                .Include(x => x.AckItems)
                .Include(x => x.AckTd5s);
        }
    }
}
