using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Angular2WebpackVisualStudio.Models
{
    public class Link{
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
         [BsonElement("name")]
        public string Name {get;set;}
         [BsonElement("desc")]
        public string Desc {get;set;}
         [BsonElement("url")]
        public string Url {get;set;}
         [BsonElement("urldesc")]
        public string UrlDesc {get;set;}
    }
}