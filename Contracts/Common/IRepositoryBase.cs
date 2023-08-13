using System.Linq.Expressions;

namespace Contracts.Common
{
    public interface IRepositoryBase<T> 
    {
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        IQueryable<T> GetAll(bool trackChanges);        
    }
}
