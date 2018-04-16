using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KinderKulturServer.Contracts;
using KinderKulturServer.Infrastructure;
using KinderKulturServer.Models;
using KinderKulturServer.Repositories.Links;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using NLog.Extensions.Logging;

namespace KinderKulturServer.Controller
{
    [Authorize(Policy = "ApiUser")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LinksController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ILinkRepository _linksRepository;
        private readonly ClaimsPrincipal _caller;

        private ILoggerManager _logger;

        public LinksController(ILinkRepository linksRepository, ILoggerManager logger, IHttpContextAccessor httpContextAccessor)
        {
            _linksRepository = linksRepository;
            _logger = logger;
            _caller = httpContextAccessor.HttpContext.User;
        }


    
        [AllowAnonymous]    
        [NoCache]
        [HttpGet]
        public Task<IEnumerable<Link>> Get()
        {
            _logger.LogInfo("Here is info message from our values controller.");
            _logger.LogDebug("Here is debug message from our values controller.");
            _logger.LogWarn("Here is warn message from our values controller.");
            _logger.LogError("Here is error message from our values controller.");
            return GetAllLinksInternal();
        }

        private async Task<IEnumerable<Link>> GetAllLinksInternal()
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
                name = value.name,
                title = value.title,
                desc = value.desc,
                url = value.url,
                urldesc = value.urldesc
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

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdate(int id, [FromBody] JsonPatchDocument<Link> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            Task<Link> existingEntity = _linksRepository.GetLink(id.ToString());

            if (existingEntity == null)
            {
                return NotFound();
            }

            Task<Link> thing = existingEntity;
            patchDoc.ApplyTo(thing.Result, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _linksRepository.UpdateLink(id.ToString(), thing.Result);

            return Ok(result.Result);
        }
    }
}
