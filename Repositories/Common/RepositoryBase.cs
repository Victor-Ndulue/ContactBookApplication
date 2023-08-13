using Contracts.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repositories.Common
{
    public class RepositoryBase<T>: IRepositoryBase<T> where T : class
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _dbSet;

        public RepositoryBase(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> GetAll( bool trackChanges) 
        {
            var query = _dbSet.AsQueryable();
            if (!trackChanges) query = query.AsNoTracking();
            return query;
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges) 
        {
            var query = _dbSet.AsQueryable();
            if(!trackChanges) query = query.AsNoTracking();
            return query.Where(expression);
        }        
    }
}
