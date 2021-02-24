using System.Collections.Generic;
using System.Threading.Tasks;
using HeroApi;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Domain;

namespace HeroApi.Implementation
{
    public class HeroService : IHeroService
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<HeroDocument> collection;

        public HeroService(MongoClient dbClient, IConfiguration config)
        {
            var dbName = config.GetValue<string>("HeroDatabaseName");
            var collectionName = "hero";
            _database = dbClient.GetDatabase(dbName);
            collection = _database.GetCollection<HeroDocument>(collectionName);
            var temp = BsonClassMap.GetRegisteredClassMaps();
            
        }
        
        public async Task<IEnumerable<HeroDocument>> GetHeroes()
        {
            var filter = Builders<HeroDocument>.Filter.Empty;
            var cursor = await collection.FindAsync(filter);
            return cursor.ToEnumerable();
        }

        public async Task<HeroDocument> InsertHero(HeroDocument hero)
        {
            await collection.InsertOneAsync(hero);
            var findOneCursor = await collection.FindAsync<HeroDocument>(db => db.id == hero.id);
            return findOneCursor.SingleOrDefault();
        }

        public async Task<HeroDocument> DeleteHero(HeroDocument hero)
        {
            await collection.DeleteOneAsync<HeroDocument>(db => db.id == hero.id);
            return hero;
        }

        public async Task<HeroDocument> UpdateHero(HeroDocument hero)
        {
            var filter = Builders<HeroDocument>.Filter.Eq(dbHero => dbHero.id, hero.id);
            var updates = Builders<HeroDocument>.Update.Set( dbHero => dbHero.name, hero.name);

            var replaceOneResult = await collection.FindOneAndUpdateAsync<HeroDocument>(filter,updates);
            return hero;
        }

        public async Task<IEnumerable<HeroDocument>> SearchHeroes(string term)
        {
            // Text search would be faster than Regex but requires a Text Index on the collection.
            var filter = Builders<HeroDocument>.Filter.Regex(dbHero => dbHero.name, $"/{term}/i");
            var cursor = await collection.FindAsync(filter);
            return cursor.ToEnumerable();
        }
        public async Task<HeroDocument> GetHero(int id)
        {
            var result = await collection.FindAsync<HeroDocument>(db => db.id == id);
            return result.FirstOrDefault();
        }
    }
}