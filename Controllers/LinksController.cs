using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KinderKulturServer.Contracts;
using KinderKulturServer.Infrastructure;
using KinderKulturServer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using NLog.Extensions.Logging;

namespace KinderKulturServer.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;
        private readonly ClaimsPrincipal _caller;
        private ILoggerManager _logger;

        public LinksController(IRepositoryWrapper repoWrapper, ILoggerManager logger, IHttpContextAccessor httpContextAccessor)
        {
            _repoWrapper = repoWrapper;
            _logger = logger;
            _caller = httpContextAccessor.HttpContext.User;
        }
    
        [AllowAnonymous]    
        [NoCache]
        [HttpGet]
        public Task<IEnumerable<LinkViewModel>> Get()
        {
            _logger.LogInfo("Here is info message from our values controller.");
            _logger.LogDebug("Here is debug message from our values controller.");
            _logger.LogWarn("Here is warn message from our values controller.");
            _logger.LogError("Here is error message from our values controller.");
            return GetAllLinksInternal();
        }

        private async Task<IEnumerable<LinkViewModel>> GetAllLinksInternal()
        {
            return await _repoWrapper.Links.FindAllAsync();
        }

        [ProducesResponseType(400)]         // BadRequest
        [ProducesResponseType(200)]         // OK
        [HttpGet("{id}", Name = nameof(GetLink))]
        public async Task<ActionResult<LinkViewModel>> GetLink(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var viewModel = await (_repoWrapper.Links.FindByIdAsync(id));

            return base.Ok(viewModel);
        }

        [ProducesResponseType(400)]         // BadRequest
        [ProducesResponseType(201)]         // Created
        [HttpPost]                          // POST api/links
        public async Task<ActionResult<LinkViewModel>>CreateLink([FromBody] LinkViewModel viewModel)
        {
            if (viewModel == null)
                return base.BadRequest();
                

            await (_repoWrapper.Links.AddModel(viewModel));

            return base.CreatedAtRoute(nameof(GetLink), new { id = viewModel.Id }, viewModel);
        }

        [ProducesResponseType(204)]         // NoContent
        [HttpPut("{id}")]                   // PUT api/links/5
        public IActionResult Update(string id, [FromBody] LinkViewModel viewModel)
        {

            _repoWrapper.Links.UpdateDbDocument(id, viewModel);
            return base.NoContent();
        }

        [ProducesResponseType(404)]         // Not Found
        [ProducesResponseType(204)]         // NoContent
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var link = _repoWrapper.Links.FindByIdAsync(id);
            if (link == null)
                return base.NotFound();
            
            _repoWrapper.Links.RemoveModel(id);
            return base.NoContent();
        }

        [ProducesResponseType(404)]         // Not Found
        [ProducesResponseType(400)]         // BadRequest
        [ProducesResponseType(200)]         // OK
        [HttpPatch("{id:int}")]             // POST api/links
        public IActionResult PartiallyUpdate(int id, [FromBody] JsonPatchDocument<LinkViewModel> patchDoc)
        {
            if (patchDoc == null)
                return base.BadRequest();

            Task<LinkViewModel> existingEntity = _repoWrapper.Links.FindByIdAsync(id.ToString());

            if (existingEntity == null)
                return base.NotFound();

            Task<LinkViewModel> link = existingEntity;
            patchDoc.ApplyTo(link.Result, ModelState);

            var result = _repoWrapper.Links.UpdateModel(id.ToString(), link.Result);

            return base.Ok(result.Result);
        }
    }
}
