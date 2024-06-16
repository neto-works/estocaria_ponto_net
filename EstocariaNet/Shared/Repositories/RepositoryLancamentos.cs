using System.Linq.Expressions;
using EstocariaNet.Models;
using EstocariaNet.Shared.Context;
using EstocariaNet.Shared.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EstocariaNet.Shared.Repositories
{
    public class RepositoryLancamentos : Repository<Lancamento>, IRepositoryLancamentos
    {
        protected new readonly AppDbContext _context;

        public RepositoryLancamentos(AppDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Lancamento>> GetLancamentosInitDataToEndDataAsync(Expression<Func<Lancamento, bool>> predicate)
        {
            return await _context.Set<Lancamento>().Where(predicate).ToListAsync();
        }

    }
}
