using EstocariaNet.Shared.Context;
using EstocariaNet.Shared.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EstocariaNet.Shared.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        async Task<IEnumerable<T>> IRepository<T>.GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        async Task<T?> IRepository<T>.GetByIdAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        async Task<T> IRepository<T>.CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        async Task<T> IRepository<T>.DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity is null)
            {
                throw new ArgumentException($"O recurso com o ID {id} não foi encontrado.");
            }

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        async Task<T> IRepository<T>.DeleteAsync(string id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new ArgumentException($"O recurso com o ID {id} não foi encontrado.");
            }

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        async Task<T> IRepository<T>.UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
