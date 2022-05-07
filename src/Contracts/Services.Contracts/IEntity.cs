using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Services.Contracts.Data
{
    public interface IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        object Id { get; set; }
    }
}
