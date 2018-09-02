using System;
using KinderKulturServer.Contracts;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace KinderKulturServer.Models.Entities
{
    public class Link : IDbModel{
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("title")]
        public string title {get;set;}
        
        [BsonElement("name")]
        public string name {get;set;}

        [BsonElement("desc")]
        public string desc {get;set;}

        [BsonElement("url")]
        public string url {get;set;}
        
        [BsonElement("urldesc")]
        public string urldesc {get;set;}

        [BsonElement("imagename")]
        public string imagename {get;set;}

        public string hostName {get;}
    }
}