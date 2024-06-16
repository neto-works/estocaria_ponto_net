using System.Linq.Expressions;
using EstocariaNet.Models;
using EstocariaNet.Shared.Context;
using EstocariaNet.Shared.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EstocariaNet.Shared.Repositories
{
    public class RepositoryRelatorios : Repository<Relatorio>, IRepositoryRelatorios
    {
        protected new readonly AppDbContext _context;

        public RepositoryRelatorios(AppDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Relatorio>> GetRelatoriosInitDataToEndDataAsync(Expression<Func<Relatorio, bool>> predicate)
        {
            return await _context.Set<Relatorio>().Where(predicate).ToListAsync();
        }

    }
}
