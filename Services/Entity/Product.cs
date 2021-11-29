using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Services.Entity
{
    [BsonIgnoreExtraElements]
    public class Product
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
