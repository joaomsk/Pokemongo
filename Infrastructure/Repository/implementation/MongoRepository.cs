using Domain.Collection;
using Domain.Model;
using Domain.System;
using Infrastructure.Settings;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Repository.implementation;

public sealed class MongoRepository<T> : IMongoRepository<T> where T : IDocument
{
    private readonly IMongoCollection<T> _collection;

    public MongoRepository(IMongoDbSettings settings)
    {
        IMongoDatabase? database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
        _collection = database.GetCollection<T>(typeof(T).GetCollectionName());
    }

    public async Task InsertOneAsync(T document)
    {
        await _collection.InsertOneAsync(document);
    }
        
    public async Task ReplaceOneAsync(T document)
    {
        FilterDefinition<T>? filter = Builders<T>.Filter.Eq(doc => doc.Id, document.Id);
        await _collection.FindOneAndReplaceAsync(filter, document);
    }
        
    public async Task<T> FindByIdAsync(string id)
    {
        return await Task.Run(() =>
        {
            ObjectId objectId = new ObjectId(id);
            FilterDefinition<T>? filter = Builders<T>.Filter.Eq(field: doc => doc.Id, objectId);

            return _collection.Find(filter).SingleOrDefaultAsync();
        });
    }

    public async Task DeleteByIdAsync(string id)
    {
        await Task.Run(() =>
        {
            ObjectId objectId = new ObjectId(id);
            FilterDefinition<T>? filter = Builders<T>.Filter.Eq(doc => doc.Id, objectId);

            _collection.FindOneAndDeleteAsync(filter);
        });
    }
}