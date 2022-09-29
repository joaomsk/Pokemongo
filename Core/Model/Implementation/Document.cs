using MongoDB.Bson;

namespace Domain.Model.Implementation;

public abstract class Document : IDocument
{
    public ObjectId Id { get; set; }
    public DateTime CreatedAt => Id.CreationTime;
}