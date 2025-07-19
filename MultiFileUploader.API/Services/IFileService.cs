namespace MultiFileUploader.API.Services
{
    public interface IFileService
    {
        Task<List<string>> UploadFilesAsync(List<IFormFile> files);
    }
}
