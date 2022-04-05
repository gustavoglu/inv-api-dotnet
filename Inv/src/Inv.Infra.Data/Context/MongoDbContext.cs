using Inv.Domain.Core.Entities;
using Inv.Domain.Interfaces.Users;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inv.Infra.Data.Context
{
    public class MongoDbContext
    {
        public MongoClient Client;
        private readonly IConfiguration _configuration;
        private IMongoDatabase _database;
        private readonly IUserAuthHelper _userAuthHelper;

        public MongoDbContext(IConfiguration configuration, IUserAuthHelper userAuthHelper)
        {
            _configuration = configuration;
            Configure();
            ConfigureConvetionsPack();
            _userAuthHelper = userAuthHelper;
        }

        public void SetPropertiesInInsert<T>(T entity) where T :Entity
        {
            entity.CreateAt = DateTime.Now;
            entity.CreateBy = _userAuthHelper.GetUsername();
        }

        public void SetPropertiesInUpdate<T>(T entity) where T : Entity
        {
            var id = entity.Id;
            var collection = GetCollection<T>();
            var entityDTO = collection.Find(entity => entity.Id == id).FirstOrDefault();

            entity.CreateAt = entityDTO.CreateAt;
            entity.CreateBy = entityDTO.CreateBy;
            entity.UpdateBy = _userAuthHelper.GetUsername();
            entity.UpdateAt = DateTime.Now;
        }

        public void SetPropertiesInDelete<T>(T entity) where T : Entity
        {
            var id = entity.Id;
            var collection = GetCollection<T>();
            var entityDTO = collection.Find(entity => entity.Id == id).FirstOrDefault();

            entity.CreateAt = entityDTO.CreateAt;
            entity.CreateBy = entityDTO.CreateBy;
            entity.UpdateBy = entityDTO.UpdateBy;
            entity.UpdateAt = entityDTO.UpdateAt;
            entity.DeleteBy = _userAuthHelper.GetUsername();
            entity.DeleteAt = DateTime.Now;
            entity.IsDeleted = true;
        }

        public virtual void ConfigureConvetionsPack()
        {
            var conventionPack = new ConventionPack {
                new CamelCaseElementNameConvention(),
                new EnumRepresentationConvention(MongoDB.Bson.BsonType.String),
                };

            ConventionRegistry.Register("camelCase", conventionPack, t => true);

        }


        public virtual void Configure()
        {
            string connString = _configuration.GetConnectionString("MongoDb");

            MongoUrl mongoUrl = new MongoUrl(connString);
            Client = new MongoClient(mongoUrl);
            _database = Client.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(typeof(T).Name.ToLower());
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
