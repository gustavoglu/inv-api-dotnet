using Inv.Domain.ObjectValues;
using System.Linq.Expressions;

namespace Inv.Domain.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        PaginationData<T> GetAll(int? page = null, int? limit = null, string sortBy = null, bool sortDesc = false);
        PaginationData<T> Search(Expression<Func<T, bool>> predicate, int? page = null, int? limit = null, string sortBy = null, bool sortDesc = false);
        T GetById(Guid id);
        T Insert(T entity);
        T Update(T entity);
        void Delete(Guid id);
        public IEnumerable<T> GetByIds(Guid[] ids);
        bool Exists(Expression<Func<T, bool>> expression);
    }
}
