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
        Task<LinkViewModel> GetLink(string id);
        Task AddLink(LinkViewModel viewModel);
        Task<IEnumerable<LinkViewModel>> GetAllLinks();
        Task<ReplaceOneResult> UpdateLinkDocument(string id, LinkViewModel viewModel);
        Task<ReplaceOneResult> UpdateLink(string id, LinkViewModel viewModel);
        Task<DeleteResult> RemoveLink(string id);
        Task<DeleteResult> RemoveAllLinks();
    }
}
