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
        private string _databaseName = "";
        private MongoServer _provider;
        private MongoDatabase _db { get { return this._provider.GetDatabase(_databaseName); } }

        public MongoRepository()
        {
            // Read the connection string from the web.config
            string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

            // Setup the provider.
            _provider = MongoServer.Create(connectionString);
            _databaseName = MongoUrl.Create(connectionString).DatabaseName;
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
            _db.GetCollection<T>(typeof(T).Name).Remove(query);
        }

        public void DeleteAll<T>() where T : class, new()
        {
            _db.GetCollection<T>(typeof(T).Name).Drop();
        }

        public T Single<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, new()
        {
            return All<T>().Where(expression).SingleOrDefault();
        }

        public IQueryable<T> All<T>() where T : class, new()
        {
            return _db.GetCollection<T>(typeof(T).Name).FindAll().AsQueryable();
        }

        public IQueryable<T> All<T>(int page, int pageSize) where T : class, new()
        {
            return PagingExtensions.Page(All<T>(), page, pageSize);
        }

        public void Add<T>(T item) where T : class, new()
        {
            _db.GetCollection<T>(typeof(T).Name).Save(item);
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
