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
    [ApiController]
    public class LinksController : ControllerBase
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


        [HttpGet("{id}", Name = nameof(GetLink))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Link>> GetLink(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var link = await (_linksRepository.GetLink(id));

            return base.Ok(link);
        }

        // POST api/links
        [HttpPost]
        public async Task<ActionResult<Link>>CreateLink([FromBody] Link value)
        {
            if (value == null)
                return base.BadRequest();
                
            var newLink = new Link()
            {
                name = value.name,
                title = value.title,
                desc = value.desc,
                url = value.url,
                urldesc = value.urldesc
            };

            await (_linksRepository.AddLink(newLink));

            return base.CreatedAtRoute(nameof(GetLink), new { id = newLink.Id }, newLink);
        }

        // PUT api/links/5
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Link value)
        {

            _linksRepository.UpdateLinkDocument(id, value);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var link = _linksRepository.GetLink(id);
            if (link == null)
                return base.NotFound();
            
            _linksRepository.RemoveLink(id);
            return new NoContentResult();
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdate(int id, [FromBody] JsonPatchDocument<Link> patchDoc)
        {
            if (patchDoc == null)
                return base.BadRequest();

            Task<Link> existingEntity = _linksRepository.GetLink(id.ToString());

            if (existingEntity == null)
                return base.NotFound();

            Task<Link> link = existingEntity;
            patchDoc.ApplyTo(link.Result, ModelState);

            var result = _linksRepository.UpdateLink(id.ToString(), link.Result);

            return base.Ok(result.Result);
        }
    }
}
