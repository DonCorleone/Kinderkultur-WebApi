using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Angular2WebpackVisualStudio.Infrastructure;
using Angular2WebpackVisualStudio.Models;
using Angular2WebpackVisualStudio.Repositories.Links;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

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

        [HttpGet("{id}", Name = "GetLink")]
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
        public IActionResult Post([FromBody] Link value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var newLink = new Link()
                {
                    desc = value.desc,
                    url = value.url,
                    urldesc = value.urldesc,
                    name = value.name
                };

            var x = _linksRepository.AddLink(newLink);

            return CreatedAtRoute("GetLink", new { id = newLink.Id }, newLink);
        }

        // PUT api/links/5
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Link value)
        {
         
            _linksRepository.UpdateLinkDocument(id, value);
            return new NoContentResult();
        }

             // DELETE api/notes/23243423
        // [HttpDelete("{id}")]
        // public void Delete(string id)
        // {
        //     _linksRepository.RemoveLink(id);
        // }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var todo = _linksRepository.GetLink(id);
            if (todo == null)
            {
                return NotFound();
            }

            _linksRepository.RemoveLink(id);
            return new NoContentResult();
        }
    }
}
