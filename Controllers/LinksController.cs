using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KinderKulturServer.Contracts;
using KinderKulturServer.Infrastructure;
using KinderKulturServer.Models;
using KinderKulturServer.Models.Entities;
using KinderKulturServer.Repositories.Links;
using KinderKulturServer.ViewModels;
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


        /// <summary>
        /// Gets a Link
        /// </summary>
        /// <remarks>
        /// /api/vx/Links/Id
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>A Link</returns>
        /// <response code="200">Returns the Link</response>
        /// <response code="400">If Link not found</response>  
        [ProducesResponseType(400)]         // BadRequest
        [ProducesResponseType(200)]         // OK
        [HttpGet("{id}", Name = nameof(GetLink))]
        public async Task<ActionResult<Link>> GetLink(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var link = await (_linksRepository.GetLink(id));

            return base.Ok(link);
        }

        [ProducesResponseType(400)]         // BadRequest
        [ProducesResponseType(201)]         // Created
        [HttpPost]                          // POST api/links
        public async Task<ActionResult<Link>>CreateLink([FromBody] LinkViewModel value)
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

        [ProducesResponseType(204)]         // NoContent
        [HttpPut("{id}")]                   // PUT api/links/5
        public IActionResult Update(string id, [FromBody] Link value)
        {

            _linksRepository.UpdateLinkDocument(id, value);
            return base.NoContent();
        }

        [ProducesResponseType(404)]         // Not Found
        [ProducesResponseType(204)]         // NoContent
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var link = _linksRepository.GetLink(id);
            if (link == null)
                return base.NotFound();
            
            _linksRepository.RemoveLink(id);
            return base.NoContent();
        }

        [ProducesResponseType(404)]         // Not Found
        [ProducesResponseType(400)]         // BadRequest
        [ProducesResponseType(200)]         // OK
        [HttpPatch("{id:int}")]             // POST api/links
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
