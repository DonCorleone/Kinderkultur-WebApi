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
        public string name {get;set;}
         [BsonElement("desc")]
        public string desc {get;set;}
         [BsonElement("url")]
        public string url {get;set;}
         [BsonElement("urldesc")]
        public string urldesc {get;set;}

        public string hostName {get;}
    }
}