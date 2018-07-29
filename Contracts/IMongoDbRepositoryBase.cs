using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace KinderKulturServer.Contracts
{
   public interface IMongoDbRepositoryBase<T> where T : class
   {
      Task<T> FindByIdAsync(string id);
      Task AddModel(T viewModel);
      Task<IEnumerable<T>> FindAllAsync();
      Task<ReplaceOneResult> UpdateDbDocument(string id, T viewModel);
      Task<ReplaceOneResult> UpdateModel(string id, T viewModel);
      Task<DeleteResult> RemoveModel(string id);
      Task<DeleteResult> RemoveAllModels();
   }
}