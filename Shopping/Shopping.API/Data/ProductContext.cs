using MongoDB.Driver;
using Shopping.API.Models;
using Shopping.API.Configuration;

namespace Shopping.API.Data
{
    public class ProductContext
    {
        private readonly IMongoDatabase _database;
        public IMongoCollection<Product> Products { get; }

        public ProductContext(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
            Products = _database.GetCollection<Product>("Products");
        }
    }
}