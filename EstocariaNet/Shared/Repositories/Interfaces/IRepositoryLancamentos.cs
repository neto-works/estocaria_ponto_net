using System.Linq.Expressions;
using EstocariaNet.Models;

namespace EstocariaNet.Shared.Repositories.Interfaces
{
    public interface IRepositoryLancamentos: IRepository<Lancamento>
    {
        
        Task<List<Lancamento>> GetLancamentosInitDataToEndDataAsync(Expression<Func<Lancamento, bool>> predicate);
    }
}
