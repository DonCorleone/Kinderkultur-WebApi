using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KinderKulturServer.Data;
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
        private readonly IMapper _mapper;

        private IMongoCollection<Link> LinksCollection
        {
            get
            {
                return _mongoDbContext.MongoDatabase.GetCollection<Link>("links");
            }
        }

        public LinkRepository(MongoDBContext mongoDBContext, IMapper mapper)
        {
            this._mongoDbContext = mongoDBContext;
            this._mapper = mapper;
        }

        public async Task AddLink(LinkViewModel viewModel)
        {
            try
            {
                var link =  _mapper.Map<Link>(viewModel);
                await LinksCollection.InsertOneAsync(link);
            }
            catch (System.Exception ex)
            {
                // ToDo: log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<LinkViewModel>> GetAllLinks()
        {
            try
            {
                var dbList = await LinksCollection.Find(_ => true).ToListAsync();
                return _mapper.Map<IEnumerable<LinkViewModel>>(dbList);
            }
            catch (System.Exception ex)
            {
                // ToDo: log or manage the exception
                throw ex;
            }
        }

        private async Task<Link> GetDbLink(string id)
        {
            var filter = Builders<Link>.Filter.Eq("Id", ObjectId.Parse(id));
            try
            {
                var dbLink = await LinksCollection
                    .Find(filter)
                    .FirstOrDefaultAsync();

                return dbLink;
            }
            catch (System.Exception ex)
            { 
                // ToDo: log or manage the exception
                throw ex;
            }
        }

        public async Task<LinkViewModel> GetLink(string id)
        {
            try
            {
                var dbLink = await GetDbLink(id);
                return _mapper.Map<LinkViewModel>(dbLink);
            }
            catch (System.Exception ex)
            {
                // ToDo: log or manage the exception
                throw ex;
            }
        }

        // Demo function - full document update
        public async Task<ReplaceOneResult> UpdateLinkDocument(string id, LinkViewModel viewModel)
        {
            var dbLink = await GetDbLink(id);
            dbLink = _mapper.Map<Link>(viewModel);

            return await UpdateLinkOnDb(id, dbLink);
        }

        public async Task<ReplaceOneResult> UpdateLink(string id, LinkViewModel viewModel)
        {
            try
            {
                var dbLink = _mapper.Map<Link>(viewModel);
                return await UpdateLinkOnDb(id, dbLink);
            }
            catch (Exception ex)
            {
                // ToDo: log or manage the exception
                throw ex;
            }
        }

        private async Task<ReplaceOneResult> UpdateLinkOnDb(string id, Link dbLink)
        {
            try
            {
                var tempId = ObjectId.Parse(id);
                return await LinksCollection
                    .ReplaceOneAsync<Link>(n => n.Id.Equals(tempId), dbLink, new UpdateOptions { IsUpsert = true });
            }
            catch (Exception ex)
            {
                // ToDo: log or manage the exception
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
                // ToDo: log or manage the exception
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
                // ToDo: log or manage the exception
                throw ex;
            }
        }
    }
}