using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Base
{
    public class BaseRepositorio<T> : IBaseRepository<T> where T : class
    {
        private readonly DataContext _dataContex;

        public BaseRepositorio(DataContext dataContex)
        {
            _dataContex = dataContex;
        }

        public async Task Delete(int Id)
        {
            var entity = await GetById(Id);
            _dataContex.Set<T>().Remove(entity);
            await _dataContex.SaveChangesAsync();

        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dataContex.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(int Id)
        {
           return await _dataContex.Set<T>().FindAsync(Id);
        }       

        public async Task Insert(T entity)
        {
            await _dataContex.Set<T>().AddAsync(entity);
            await _dataContex.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _dataContex.Set<T>().Update(entity);
            await _dataContex.SaveChangesAsync();
        }
    }
}
