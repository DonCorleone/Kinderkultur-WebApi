using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ImageWriter.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KinderKulturServer.Handler
{
    /// <summary>
    /// Client Side Image Handler Interface
    /// </summary>
    public interface IImageHandler
    {
        Task<IActionResult> UploadImage(IEnumerable<IFormFile> file);
    }

    /// <summary>
    /// Client Side Image Handler
    /// </summary>
    public class ImageHandler : IImageHandler
    {
        private readonly IImageWriter _imageWriter;
        private readonly IWebHostEnvironment _appEnvironment;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="imageWriter"></param>
        /// <param name="appEnvironment"></param>
        public ImageHandler(IImageWriter imageWriter, IWebHostEnvironment appEnvironment)
        {
            this._imageWriter = imageWriter;
            this._appEnvironment = appEnvironment;
        }

        /// <summary>
        /// Upload Image Request
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task<IActionResult> UploadImage(IEnumerable<IFormFile> files)
        {
            var result = new StringBuilder();
            foreach (var file in files)
            {
                result.Append(await _imageWriter.UploadImage(file, _appEnvironment.WebRootPath + "/images/"));

            }
            return new ObjectResult(JsonConvert.SerializeObject(result.ToString()));
        }
    }
}