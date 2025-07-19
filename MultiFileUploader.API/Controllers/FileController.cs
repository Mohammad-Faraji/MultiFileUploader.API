using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiFileUploader.API.Services;

namespace MultiFileUploader.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private IFileService _fileService;

        public FileController(IFileService fileService)
        {
                _fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            if (files == null || !files.Any())
                return BadRequest("No files received");

            var uploadedFiles = await _fileService.UploadFilesAsync(files);
            return Ok(uploadedFiles);
        }

        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> Download(string fileName)
        {
            var fileStream = await _fileService.GetFileAsync(fileName);
            if (fileStream == null)
                return NotFound();

            return File(fileStream, "application/octet-stream", fileName);
        }

        [HttpDelete("delete/{fileName}")]
        public async Task<IActionResult> Delete(string fileName)
        {
            var result = await _fileService.DeleteFileAsync(fileName);
            if (!result)
                return NotFound("File not found");

            return Ok("File deleted");
        }



        [HttpGet("list")]
        public IActionResult ListFiles()
        {
            var files = _fileService.ListAllFiles();
            return Ok(files);
        }
    }
}
