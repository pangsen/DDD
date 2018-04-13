using System;
using System.Collections.Generic;
using DDD.Core.QueryService;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DDD.Mongo
{
    public class MongoReadModeRepository<T> : IReadModeRepository<T> where T : ReadMode
    {
        private readonly IMongoCollection<T> _collection;
        public MongoReadModeRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("test");
            _collection = database.GetCollection<T>(typeof(T).Name);
        }
        public T GetById(Guid id)
        {
            return _collection.Find(Builders<T>.Filter.Eq(a => a.Id, id)).FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return _collection.Find(new BsonDocument()).ToList();
        }

        public void Save(T t)
        {
            _collection.ReplaceOne(Builders<T>.Filter.Eq(a => a.Id, t.Id), t, new UpdateOptions { IsUpsert = true });
        }
    }
}
