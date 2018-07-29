using System.Collections.Generic;
using System.Threading.Tasks;
using KinderKulturServer.Models;
using KinderKulturServer.Models.Entities;
using KinderKulturServer.ViewModels;
using MongoDB.Bson;
using MongoDB.Driver;

namespace KinderKulturServer.Contracts
{
    public interface ILinkRepository
    {
        Task<LinkViewModel> FindByIdAsync(string id);
        Task AddModel(LinkViewModel viewModel);
        Task<IEnumerable<LinkViewModel>> FindAllAsync();
        Task<ReplaceOneResult> UpdateDbDocument(string id, LinkViewModel viewModel);
        Task<ReplaceOneResult> UpdateModel(string id, LinkViewModel viewModel);
        Task<DeleteResult> RemoveModel(string id);
        Task<DeleteResult> RemoveAllModels();
    }
}
