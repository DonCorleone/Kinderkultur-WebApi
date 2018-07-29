using AutoMapper;
using KinderKulturServer.Contracts;
using KinderKulturServer.Data;

namespace KinderKulturServer.Repositories
{
   /// https://code-maze.com/net-core-web-development-part4/
   public class RepositoryWrapper : IRepositoryWrapper
   {
      private MongoDBContext _repoContext;
      private IMapper _mapper;
      private ILinkRepository _links;

      public ILinkRepository Links
      {
         get
         {
            if (_links == null)
            {
               _links = new LinkRepository(_repoContext, _mapper);
            }

            return _links;
         }
      }

      public RepositoryWrapper(MongoDBContext repositoryContext, IMapper mapper)
      {
         _repoContext = repositoryContext;
         _mapper = mapper;
      }
   }
}