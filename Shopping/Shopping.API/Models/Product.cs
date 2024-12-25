using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shopping.API.Models
{
   public class Product
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("Name")]
    public string? Name { get; set; }

    [BsonElement("Price")]
    public decimal Price { get; set; }

    [BsonElement("Description")]
    public string? Description { get; set; }

    [BsonElement("Category")]
    public string? Category { get; set; }

    [BsonElement("ImageFile")]
    public string? ImageFile { get; set; }  // Nullable if not always required
}

}
