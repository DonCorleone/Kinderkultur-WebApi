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
    /// <summary>
    /// Repository for Link-Objects in Mongo DB
    /// </summary>
    /// <typeparam name="LinkViewModel"></typeparam>
    /// <typeparam name="Link"></typeparam>
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
    }
}