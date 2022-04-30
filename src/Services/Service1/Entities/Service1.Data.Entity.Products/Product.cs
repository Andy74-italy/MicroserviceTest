using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Services.Contracts.Data;

namespace Service1.Data.Entity
{
    public class Product : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public object Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }
        public string Category { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
    }
}
