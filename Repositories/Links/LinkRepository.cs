using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Angular2WebpackVisualStudio.Data;
using Angular2WebpackVisualStudio.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Angular2WebpackVisualStudio.Repositories.Links
{
    public class LinkRepository : ILinkRepository
    {

        private readonly LinkContext _context = null;
        public LinkRepository(IOptions<Settings> settings)
        {
            _context = new LinkContext(settings);
        }

        public async Task AddLink(Link item)
        {
            try
            {
                await _context.Links.InsertOneAsync(item);
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
                return await _context.Links.Find(_ => true).ToListAsync();
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
                return await _context.Links
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
            item.Desc = body.Desc;
            item.Name = body.Name;
            item.Url = body.Url;
            item.UrlDesc = body.UrlDesc;
            
            return await UpdateLink(id, item);
        }

        public async Task<ReplaceOneResult> UpdateLink(string id, Link item)
        {
            try
            {
                var tempId = ObjectId.Parse(id);
                return await _context.Links
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
                return await _context.Links.DeleteOneAsync(filter);
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
                return await _context.Links.DeleteManyAsync(new BsonDocument());
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}