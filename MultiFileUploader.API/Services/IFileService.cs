namespace MultiFileUploader.API.Services
{
    public interface IFileService
    {
        Task<List<string>> UploadFilesAsync(List<IFormFile> files);
        Task<FileStream?> GetFileAsync(string fileName);
        Task<bool> DeleteFileAsync(string fileName);
    }
}
