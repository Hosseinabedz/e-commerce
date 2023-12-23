using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericReposiroty<T> where T : BaseEntity
    {
        #region Ctor
        public GenericRepository(StoreDbContext storeDbContext)
        {
            _context = storeDbContext;
        }

        public StoreDbContext _context { get; }
        #endregion


        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.f
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
