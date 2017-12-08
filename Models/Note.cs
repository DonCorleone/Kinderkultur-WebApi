using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Angular2WebpackVisualStudio.Models
{
    public class Note
    {
        [BsonId]
        public string Id { get; set; }
        
        [BsonElement("Body")]
        public string Body { get; set; } = string.Empty;

        [BsonElement("UpdatedOn")]
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        [BsonElement("CreatedOn")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [BsonElement("UserId")]
        public int UserId { get; set; } = 0;
    }
}