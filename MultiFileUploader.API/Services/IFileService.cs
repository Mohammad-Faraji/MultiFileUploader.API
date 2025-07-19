namespace MultiFileUploader.API.Services
{
    public interface IFileService
    {
        Task<string> UploadFilesAsync(IFormFile file, IWebHostEnvironment env);
    }
}
