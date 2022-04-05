using Inv.Domain.Core.Entities;
using Inv.Domain.Interfaces.Repositories;
using Inv.Domain.ObjectValues;
using Inv.Infra.Data.Context;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Infra.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly MongoDbContext Context;
        protected IMongoCollection<T> Collection;
   
        protected FilterDefinition<T> FilterDefault { get { return Builders<T>.Filter.Eq(e => e.IsDeleted, false); } }
        public Repository(MongoDbContext context)
        {
            Context = context;
            Collection = Context.GetCollection<T>();
           
        }
        public void Delete(Guid id)
        {
            var entity = GetById(id);
            Context.SetPropertiesInDelete(entity);
            Update(entity);
        }


        protected void SetPagination(IFindFluent<T, T> Find, int? page = null, int? limit = null)
        {
            if (Find == null || !(page.HasValue && limit.HasValue)) return;
            Find.Limit(limit.Value).Skip((page.Value - 1) * limit.Value);
        }

        protected void SetSort(IFindFluent<T, T> Find, string sortBy = null, bool sortDesc = false)
        {
            if (Find == null || string.IsNullOrEmpty(sortBy)) return;
            Find.Sort(sortDesc ? Builders<T>.Sort.Descending(sortBy) : Builders<T>.Sort.Ascending(sortBy));
        }

        public PaginationData<T> GetAll(int? page = null, int? limit = null, string sortBy = null, bool sortDesc = false)
        {
            IFindFluent<T, T> Find = Collection.Find(FilterDefault);
            long total = Find.CountDocuments();
            SetPagination(Find, page, limit);
            SetSort(Find, sortBy, sortDesc);
            return new PaginationData<T>(Find.ToList(), limit, page, total);

        }

        public IEnumerable<T> GetByIds(Guid[] ids)
        {
            var idsFilter = Builders<T>.Filter.In(e => e.Id, ids);
            return Collection.Find(idsFilter).ToList();
        }

        public T GetById(Guid id)
        {
            var idFilter = Builders<T>.Filter.Eq(e => e.Id, id);
            return Collection.Find(idFilter).FirstOrDefault();
        }

        public T Insert(T entity)
        {
            Context.SetPropertiesInInsert(entity);
            Collection.InsertOne(entity);
            return GetById(entity.Id);
        }

        public PaginationData<T> Search(Expression<Func<T, bool>> predicate, int? page = null, int? limit = null, string sortBy = null, bool sortDesc = false)
        {
            var expressoinFilter = Builders<T>.Filter.Where(predicate);

            IFindFluent<T, T> Find = Collection.Find(FilterDefault & expressoinFilter);
            long total = Find.CountDocuments();
            SetPagination(Find, page, limit);
            SetSort(Find, sortBy, sortDesc);
            return new PaginationData<T>(Find.ToList(), limit, page, total);
        }


        public virtual bool Exists(Expression<Func<T, bool>> expression)
        {
            var expressionFilter = Builders<T>.Filter.Where(expression);
            return Collection.CountDocuments(FilterDefault & expressionFilter) > 0;
        }

        public T Update(T entity)
        {
            Context.SetPropertiesInUpdate(entity);
            Collection.ReplaceOne(e => e.Id == entity.Id, entity);
            return GetById(entity.Id);
        }
    }
}
