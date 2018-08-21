using System.Threading.Tasks;
using ImageWriter.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KinderKulturServer.Handler
{
    public interface IImageHandler
    {
        Task<IActionResult> UploadImage(IFormFile file);
    }

    public class ImageHandler : IImageHandler
    {
        private readonly IImageWriter _imageWriter;
        private readonly IHostingEnvironment _appEnvironment;

        public ImageHandler(IImageWriter imageWriter, IHostingEnvironment appEnvironment)
        {
            this._imageWriter = imageWriter;
            this._appEnvironment = appEnvironment;
        }

        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var result = await _imageWriter.UploadImage(file, _appEnvironment.WebRootPath + "/images/");
            return new ObjectResult(JsonConvert.SerializeObject(result));
        }
    }
}