using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoRepository.Entities;

public abstract class BaseEntity<T>
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; }
}