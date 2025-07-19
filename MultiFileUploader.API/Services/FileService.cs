
namespace MultiFileUploader.API.Services
{
    public class FileService : IFileService
    {

        private readonly string _uploadPath;

        public FileService(IWebHostEnvironment env)
        {
            _uploadPath = Path.Combine(env.WebRootPath, "uploads");
            if (!Directory.Exists(_uploadPath))
                Directory.CreateDirectory(_uploadPath);
        }

        public async Task<bool> DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_uploadPath, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }

        public async Task<FileStream?> GetFileAsync(string fileName)
        {
            var filePath = Path.Combine(_uploadPath, fileName);

            if (!File.Exists(filePath))
                return null;

            return new FileStream(filePath, FileMode.Open,FileAccess.Read);
        }

        public async Task<List<string>> UploadFilesAsync(List<IFormFile> files)
        {
            var savedFiles = new List<string>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var filePath = Path.Combine(_uploadPath, file.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    savedFiles.Add(file.FileName);
                }
            }

            return savedFiles;
        }
    }
}
