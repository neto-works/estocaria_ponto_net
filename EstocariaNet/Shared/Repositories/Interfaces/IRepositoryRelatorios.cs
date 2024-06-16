using System.Linq.Expressions;
using EstocariaNet.Models;

namespace EstocariaNet.Shared.Repositories.Interfaces
{
    public interface IRepositoryRelatorios:IRepository<Relatorio>
    {
        
        Task<List<Relatorio>> GetRelatoriosInitDataToEndDataAsync(Expression<Func<Relatorio, bool>> predicate);
    }
}
