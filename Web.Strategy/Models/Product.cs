using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Strategy.Models
{
    public class Product
    {
        // Normalde Entity Framework Id ismini gördüğünde ilgili alanı primary key olarak işaretler.Biz yine de attribute olarak veriyoruz.
        [Key]
        // Aşağıdaki attribute'te MongoDb için veriliyor.(Id'yi primary key olarak algılaması için)
        [BsonId]
        // Aşağıdaki attribute'te tipini belirtiyoruz ki, MongoDb'den ilgili alan string olarak gelsin.
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        [BsonRepresentation(BsonType.Decimal128)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string UserId { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedDate { get; set; }
    }
}
