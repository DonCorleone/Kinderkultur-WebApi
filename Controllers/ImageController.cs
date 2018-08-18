using System.Threading.Tasks;
using KinderKulturServer.Handler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KinderKulturServer.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]	public class ImagesController : Controller
	{
		private readonly IImageHandler _imageHandler;

		public ImagesController(IImageHandler imageHandler)
		{
			_imageHandler = imageHandler;
		}

		/// <summary>
		/// Uploads an image to the server.
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		public async Task<IActionResult> UploadImage(IFormFile file)
		{
			return await _imageHandler.UploadImage(file);
		}
	}
}