using KinderKulturServer.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace KinderKulturServer.Data
{
    public class LinkContext
    {
        private readonly IMongoDatabase _database = null;

        public LinkContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Link> Links
        {
            get
            {
                return _database.GetCollection<Link>("links");
            }
        }
    }
}