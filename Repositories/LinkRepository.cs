using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KinderKulturServer.Contracts;
using KinderKulturServer.Data;
using KinderKulturServer.Models.Entities;
using KinderKulturServer.ViewModels;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace KinderKulturServer.Repositories
{
    public class LinkRepository : MongoDbRepositoryBase<LinkViewModel, Link>, ILinkRepository
    {
        public LinkRepository(MongoDBContext mongoDbContext, IMapper mapper) 
            : base(mongoDbContext, mapper)
        { 
        }
        protected override IMongoCollection<Link> Collection
        {
            get
            {
                return MongoDbContext.MongoDatabase.GetCollection<Link>("links");
            }
        }

        // public async Task AddLink(LinkViewModel viewModel)
        // {
        //     try
        //     {
        //         var link = Mapper.Map<Link>(viewModel);
        //         await Collection.InsertOneAsync(link);
        //     }
        //     catch (System.Exception ex)
        //     {
        //         // ToDo: log or manage the exception
        //         throw ex;
        //     }
        // }

        // public async Task<IEnumerable<LinkViewModel>> GetAllLinks()
        // {
        //     try
        //     {
        //         var dbList = await LinksCollection.Find(_ => true).ToListAsync();
        //         return Mapper.Map<IEnumerable<LinkViewModel>>(dbList);
        //     }
        //     catch (System.Exception ex)
        //     {
        //         // ToDo: log or manage the exception
        //         throw ex;
        //     }
        // }

        // private async Task<Link> GetDbLink(string id)
        // {
        //     var filter = Builders<Link>.Filter.Eq("Id", ObjectId.Parse(id));
        //     try
        //     {
        //         var dbLink = await Collection
        //             .Find(filter)
        //             .FirstOrDefaultAsync();

        //         return dbLink;
        //     }
        //     catch (System.Exception ex)
        //     {
        //         // ToDo: log or manage the exception
        //         throw ex;
        //     }
        // }

        // public async Task<LinkViewModel> FindByIdAsync(string id)
        // {
        //     try
        //     {
        //         var dbLink = await GetDbLink(id);
        //         return Mapper.Map<LinkViewModel>(dbLink);
        //     }
        //     catch (System.Exception ex)
        //     {
        //         // ToDo: log or manage the exception
        //         throw ex;
        //     }
        // }

        // Demo function - full document update
        // public async Task<ReplaceOneResult> UpdateLinkDocument(string id, LinkViewModel viewModel)
        // {
        //     var dbLink = await FindByIdAsync(id);
        //     dbLink = Mapper.Map<Link>(viewModel);

        //     return await UpdateLinkOnDb(id, dbLink);
        // }

        // public async Task<ReplaceOneResult> UpdateLink(string id, LinkViewModel viewModel)
        // {
        //     try
        //     {
        //         var dbLink = Mapper.Map<Link>(viewModel);
        //         return await UpdateDbDocument(id, dbLink);
        //     }
        //     catch (Exception ex)
        //     {
        //         // ToDo: log or manage the exception
        //         throw ex;
        //     }
        // }

        // private async Task<ReplaceOneResult> UpdateLinkOnDb(string id, Link dbLink)
        // {
        //     try
        //     {
        //         var tempId = ObjectId.Parse(id);
        //         return await Collection
        //             .ReplaceOneAsync<Link>(n => n.Id.Equals(tempId), dbLink, new UpdateOptions { IsUpsert = true });
        //     }
        //     catch (Exception ex)
        //     {
        //         // ToDo: log or manage the exception
        //         throw ex;
        //     }
        // }
    //     public async Task<DeleteResult> RemoveModel(string id)
    //     {
    //         try
    //         {
    //             var filter = Builders<Link>.Filter.Eq("Id", ObjectId.Parse(id));
    //             return await Collection.DeleteOneAsync(filter);
    //         }
    //         catch (Exception ex)
    //         {
    //             // ToDo: log or manage the exception
    //             throw ex;
    //         }
    //     }
    //     public async Task<DeleteResult> RemoveAllModels()
    //     {
    //         try
    //         {
    //             return await Collection.DeleteManyAsync(new BsonDocument());
    //         }
    //         catch (Exception ex)
    //         {
    //             // ToDo: log or manage the exception
    //             throw ex;
    //         }
    //     }
    }
}