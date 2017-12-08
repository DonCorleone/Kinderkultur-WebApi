using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angular2WebpackVisualStudio.Infrastructure;
using Angular2WebpackVisualStudio.Models;
using Angular2WebpackVisualStudio.Repositories.Links;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Angular2WebpackVisualStudio.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class LinksController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ILinkRepository _linksRepository;

        public LinksController(ILinkRepository linksRepository)
        {
            _linksRepository = linksRepository;
        }

        [NoCache]
        [HttpGet]
        public Task<IEnumerable<Link>> Get()
        {
            return GetLinkInternal();
        }

        private async Task<IEnumerable<Link>> GetLinkInternal()
        {
            return await _linksRepository.GetAllLinks();
        }

        [HttpGet("{id}")]
        public Task<Link> GetLink(string id)
        {

            return GetLinkByIdInternal(id);
        }

        private async Task<Link> GetLinkByIdInternal(string id)
        {

            return await (_linksRepository.GetLink(id));
        }

        // POST api/links
        [HttpPost]
        public void Post([FromBody] Link value)
        {

            _linksRepository.AddLink(new Link()
            {
                Desc = value.Desc,
                Url = value.Url,
                UrlDesc = value.UrlDesc,
                Name = value.Name
            }
            );
        }

        // PUT api/links/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]Link value)
        {
            _linksRepository.UpdateLinkDocument(id, value);
        }

             // DELETE api/notes/23243423
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _linksRepository.RemoveLink(id);
        }
    }
}
