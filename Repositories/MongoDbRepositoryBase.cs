using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KinderKulturServer.Contracts;
using KinderKulturServer.Data;
using KinderKulturServer.Models.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace KinderKulturServer.Repositories
{
    public abstract class MongoDbRepositoryBase<TViewModel, UDbModel> 
        : IMongoDbRepositoryBase<TViewModel> where TViewModel : class where UDbModel : class, IDbModel
    {
        abstract protected IMongoCollection<UDbModel> Collection{ get; }        
        protected MongoDBContext MongoDbContext { get; set; }
        protected IMapper Mapper { get; set; }

        public MongoDbRepositoryBase(MongoDBContext mongoDbContext, IMapper mapper)
        {
            this.MongoDbContext = mongoDbContext;
            this.Mapper = mapper;
        }

        public async Task<TViewModel> FindByIdAsync(string id)
        {
            try
            {
                var dbModel = await FindDbModelByIdAsync(id);                
                return Mapper.Map<TViewModel>(dbModel);
            }
            catch (System.Exception ex)
            {
                // ToDo: log or manage the exception
                throw ex;
            }
        }
        private async Task<UDbModel> FindDbModelByIdAsync(string id)
        {
            var filter = Builders<UDbModel>.Filter.Eq("Id", ObjectId.Parse(id));
            try
            {
                return  await Collection
                    .Find(filter)
                    .FirstOrDefaultAsync();
            }
            catch (System.Exception ex)
            {
                // ToDo: log or manage the exception
                throw ex;
            }
        }
        public async Task AddModel(TViewModel viewModel)
        {
            try
            {
                var dbModel = Mapper.Map<UDbModel>(viewModel);
                await Collection.InsertOneAsync(dbModel);
            }
            catch (System.Exception ex)
            {
                // ToDo: log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<TViewModel>> FindAllAsync()
        {
            try
            {
                var dbList = await Collection.Find(_ => true).ToListAsync();
                return Mapper.Map<IEnumerable<TViewModel>>(dbList);
            }
            catch (System.Exception ex)
            {
                // ToDo: log or manage the exception
                throw ex;
            }
        }

        public async Task<ReplaceOneResult> UpdateDbDocument(string id, TViewModel viewModel)
        {
            var dbModel = await FindDbModelByIdAsync(id);
            dbModel = Mapper.Map<UDbModel>(viewModel);

            return await UpdateModelOnDb(id, dbModel);
        }
        private async Task<ReplaceOneResult> UpdateModelOnDb(string id, UDbModel dbModel)
        {
            try
            {
                var tempId = ObjectId.Parse(id);
                return await Collection
                    .ReplaceOneAsync<UDbModel>(n => n.Id.Equals(tempId), dbModel, new ReplaceOptions { IsUpsert = true });
            }
            catch (Exception ex)
            {
                // ToDo: log or manage the exception
                throw ex;
            }
        }
        public Task<ReplaceOneResult> UpdateModel(string id, TViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }

        public async Task<DeleteResult> RemoveModel(string id)
        {
            try
            {
                var filter = Builders<UDbModel>.Filter.Eq("Id", ObjectId.Parse(id));
                return await Collection.DeleteOneAsync(filter);
            }
            catch (Exception ex)
            {
                // ToDo: log or manage the exception
                throw ex;
            }
        }

        public async Task<DeleteResult> RemoveAllModels()
        {
            try
            {
                return await Collection.DeleteManyAsync(new BsonDocument());
            }
            catch (Exception ex)
            {
                // ToDo: log or manage the exception
                throw ex;
            }
        }
    }
}