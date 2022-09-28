using Files.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace NormaliseAudio.Api.Controllers
{
    public class AudioController : Controller
    {
        private readonly IFileCreator _fileCreator;
        private readonly IBaseFileConfig _baseFileConfig;

        public AudioController(IFileCreator fileCreator, IBaseFileConfig baseFileConfig)
        {
            _fileCreator = fileCreator ?? throw new ArgumentNullException(nameof(fileCreator));
            _baseFileConfig = baseFileConfig ?? throw new ArgumentNullException(nameof(baseFileConfig));
        }

        [HttpPost("normalise", Name = "Normalise")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Normalise(IFormFile formFile, CancellationToken cancellationToken)
        {
            if (CheckIfAcceptedAudioFile(formFile))
            {
                _baseFileConfig.InputFileName = formFile.FileName;
                var stream = new MemoryStream(new byte[formFile.Length]);
                await formFile.CopyToAsync(stream, cancellationToken);
                return Ok(await _fileCreator.CreateFromStream(stream));
            }
            else
            {
                return BadRequest(new { message = "Invalid file extension. Accepted types are: wav, mp3 and aiff. " });
            }

            string filename = "File.pdf";
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "/Path/To/File/" + filename;
            byte[] filedata = System.IO.File.ReadAllBytes(filepath);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = filename,
                Inline = true,
            };

            Response.Headers.Append("Content-Disposition", cd.ToString());
            Response.Headers.ContentType = formFile.ContentType;            

            return File(filedata, formFile.ContentType);
        }

        private bool CheckIfAcceptedAudioFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".wav" || extension == ".mp3"); // Change the extension based on your need
        }

        private async Task<bool> WriteFile(IFormFile file)
        {
            bool isSaveSuccess = false;
            string fileName;
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                fileName = DateTime.Now.Ticks + extension; //Create a new Name for the file due to security reasons.

                var pathBuilt = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");

                if (!Directory.Exists(pathBuilt))
                {
                    Directory.CreateDirectory(pathBuilt);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files",
                   fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                isSaveSuccess = true;
            }
            catch (Exception e)
            {
                //log error
            }

            return isSaveSuccess;
        }
    }
}
