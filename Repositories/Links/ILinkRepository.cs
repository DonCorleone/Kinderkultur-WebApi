using System.Collections.Generic;
using System.Threading.Tasks;
using Angular2WebpackVisualStudio.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Angular2WebpackVisualStudio.Repositories.Links
{
    public interface ILinkRepository
    {
        Task<Link> GetLink(string id);
        Task AddLink(Link item);
        //  void Delete(int id);
        //  Link Update(int id, Link item);
        Task<IEnumerable<Link>> GetAllLinks();
        // int Count();

        // demo interface - full document update
        Task<ReplaceOneResult> UpdateLinkDocument(string id, Link body);

        Task<ReplaceOneResult> UpdateLink(string id, Link body);

        Task<DeleteResult> RemoveLink(string id);

        Task<DeleteResult> RemoveAllLinks();
    }
}
