using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KinderKulturServer.Data;
using KinderKulturServer.Models;
using KinderKulturServer.Models.Entities;
using KinderKulturServer.ViewModels;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace KinderKulturServer.Repositories.Links
{
    public class LinkRepository : ILinkRepository
    {

        private readonly MongoDBContext _mongoDbContext = null;
        private IMongoCollection<Link> LinksCollection{get { 
                return _mongoDbContext.MongoDatabase.GetCollection<Link>("links");
            }
        }
            
        
        public LinkRepository(MongoDBContext mongoDBContext)
        {
            _mongoDbContext = mongoDBContext;
        }

        public async Task AddLink(Link item)
        {
            try
            {
                await LinksCollection.InsertOneAsync(item);
            }
            catch (System.Exception ex)
            {
                
                throw ex;
            }
        }

        public async Task<IEnumerable<Link>> GetAllLinks()
        {
            try
            {
                return await LinksCollection.Find(_ => true).ToListAsync();
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Link> GetLink(string id)
        {
            //    return this._collection.Find(new BsonDocument { { "_id", new ObjectId(id) } }).FirstAsync().Result;

            var filter = Builders<Link>.Filter.Eq("Id", ObjectId.Parse(id));
            try
            {
                return await LinksCollection
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        // Demo function - full document update
        public async Task<ReplaceOneResult> UpdateLinkDocument(string id, Link body)
        {
            var item = await GetLink(id) ?? new Link();
            item.name = body.name;
            item.title = body.title;
            item.desc = body.desc;
            item.url = body.url;
            item.urldesc = body.urldesc;
            
            return await UpdateLink(id, item);
        }

        public async Task<ReplaceOneResult> UpdateLink(string id, Link item)
        {
            try
            {
                var tempId = ObjectId.Parse(id);
                return await LinksCollection
                            .ReplaceOneAsync<Link>(n => n.Id.Equals(tempId)
                                            , item
                                            , new UpdateOptions { IsUpsert = true });
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
        public async Task<DeleteResult> RemoveLink(string id)
        {
            try
            {
                var filter = Builders<Link>.Filter.Eq("Id", ObjectId.Parse(id));
                return await LinksCollection.DeleteOneAsync(filter);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
        public async Task<DeleteResult> RemoveAllLinks()
        {
            try
            {
                return await LinksCollection.DeleteManyAsync(new BsonDocument());
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}