using FFMpeg.Abstractions;
using Files.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace NormaliseAudio.Api.Controllers
{
    public class AudioController : Controller
    {
        private readonly IFileCreator _fileCreator;
        private readonly IFileRemover _fileRemover;
        private readonly ILUFSProvider _lufsProvider;

        public AudioController(IFileCreator fileCreator, IFileRemover fileRemover, ILUFSProvider lufsProvider)
        {
            _fileCreator = fileCreator ?? throw new ArgumentNullException(nameof(fileCreator));
            _fileRemover = fileRemover ?? throw new ArgumentNullException(nameof(fileRemover));
            _lufsProvider = lufsProvider ?? throw new ArgumentNullException(nameof(lufsProvider));
        }

        [HttpPost("normalise", Name = "Normalise")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(73400320)]
        public async Task<IActionResult> Normalise(IFormFile formFile, CancellationToken cancellationToken)
        {
            if (CheckIfAcceptedAudioFile(formFile))
            {
                string fileLocation;
                using (var stream = new MemoryStream(new byte[formFile.Length]))
                {
                    await formFile.CopyToAsync(stream, cancellationToken);
                    fileLocation = _fileCreator.CreateFromStream(stream, formFile.FileName);
                }
                var outputLocation = _lufsProvider.AdjustLufsOfInput(fileLocation);
                FileInfo returnFileInfo = new FileInfo(outputLocation);

                byte[] filedata = System.IO.File.ReadAllBytes(outputLocation);

                try
                {
                    _fileRemover.RemoveByPath(fileLocation);
                    _fileRemover.RemoveByPath(outputLocation);
                }
                catch(Exception ex)
                {
                    throw;
                }
                
                var cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = returnFileInfo.Name,
                    Inline = true,
                };

                Response.Headers.Append("Content-Disposition", cd.ToString());
                Response.Headers.ContentType = "audio/mpeg";

                return File(filedata, formFile.ContentType);
            }
            else
            {
                return BadRequest(new { message = "Invalid file extension. Accepted types are: wav, mp3 and aiff. " });
            }
        }

        private bool CheckIfAcceptedAudioFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".wav" || extension == ".mp3"); // Change the extension based on your need
        }
    }
}
