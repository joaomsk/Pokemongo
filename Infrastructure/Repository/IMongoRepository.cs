using Domain.Model;

namespace Infrastructure.Repository;

public interface IMongoRepository<TDocument> where TDocument : IDocument
{
    Task InsertOneAsync(TDocument document);
    Task<TDocument> FindByIdAsync(string id);
    Task ReplaceOneAsync(TDocument document);
    Task DeleteByIdAsync(string id);
}