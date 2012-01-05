using System;
using System.Collections.Generic;
using System.Linq;
using MongoDragons.Repository.Interface;
using MongoDB.Driver;
using MongoDragons.Repository.Helpers;
using System.Configuration;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace MongoDragons.Database.Concrete
{
    public class MongoRepository : IRepository
    {
        private MongoServer _provider;
        public MongoDatabase DB { get { return this._provider.GetDatabase("dragons"); } }

        public MongoRepository()
        {
            // Read the connection string from the web.config
            _provider = MongoServer.Create(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
        }

        public void Delete<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, new()
        {
            var items = All<T>().Where(expression);
            foreach (T item in items)
            {
                Delete(item);
            }
        }

        public void Delete<T>(T item) where T : class, new()
        {
            // Get the Id value for the class.
            object value = GetPropValue(item, "Id");

            // Create the search query by Id.
            var query = Query.EQ("_id", ObjectId.Parse(value.ToString()));

            // Remove the object.
            DB.GetCollection<T>(typeof(T).Name).Remove(query);
        }

        public void DeleteAll<T>() where T : class, new()
        {
            DB.GetCollection<T>(typeof(T).Name).Drop();
        }

        public T Single<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, new()
        {
            return All<T>().Where(expression).SingleOrDefault();
        }

        public IQueryable<T> All<T>() where T : class, new()
        {
            return DB.GetCollection<T>(typeof(T).Name).FindAll().AsQueryable();
        }

        public IQueryable<T> All<T>(int page, int pageSize) where T : class, new()
        {
            return PagingExtensions.Page(All<T>(), page, pageSize);
        }

        public void Add<T>(T item) where T : class, new()
        {
            DB.GetCollection<T>(typeof(T).Name).Save(item);
        }

        public void Add<T>(IEnumerable<T> items) where T : class, new()
        {
            foreach (T item in items)
            {
                Add(item);
            }
        }

        public void Dispose()
        {
        }

        private object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
