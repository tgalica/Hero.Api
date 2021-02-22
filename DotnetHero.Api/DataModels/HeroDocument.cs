using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace HeroApi 
{
    public class HeroDocument: Domain.Hero
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        internal ObjectId MongoId { get; set; }

    }
}