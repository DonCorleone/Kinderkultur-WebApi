using KinderKulturServer.Models;
using KinderKulturServer.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace KinderKulturServer.Data
{
    public class MongoDBContext : DbContext
    {
        public IMongoDatabase MongoDatabase { get; set;}

        public MongoDBContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                MongoDatabase = client.GetDatabase(settings.Value.Database);
        }
    }
}