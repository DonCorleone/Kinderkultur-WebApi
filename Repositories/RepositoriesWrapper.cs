using AutoMapper;
using KinderKulturServer.Contracts;
using KinderKulturServer.Data;

namespace KinderKulturServer.Repositories
{
    /// <summary>
    /// Wrapper for Repositories
    /// </summary>
    /// <see cref="https://code-maze.com/net-core-web-development-part4/"></see>
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
                    _links = new LinkRepository(_repoContext, _mapper);

                return _links;
            }
        }
        /// <summary>
        /// Called by DI
        /// </summary>
        /// <param name="repositoryContext"></param>
        /// <param name="mapper"></param>
        public RepositoryWrapper(MongoDBContext repositoryContext, IMapper mapper)
        {
            _repoContext = repositoryContext;
            _mapper = mapper;
        }
    }
}