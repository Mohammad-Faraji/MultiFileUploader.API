
namespace MultiFileUploader.API.Services
{
    public class FileService : IFileService
    {
             
        public async Task<string> UploadFilesAsync(IFormFile file, IWebHostEnvironment env)
        {
            if (file == null || file.Length == 0)
                return null;

            // آدرس کامل محل آپلود را همین‌جا مشخص می‌کنیم
            string uploadPath = Path.Combine(env.WebRootPath, "uploads");

            if (!Directory.Exists(uploadPath)) 
            {
                Directory.CreateDirectory(uploadPath);
            }

            // ایجاد نام یکتا برای فایل
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(uploadPath, uniqueFileName);

            // ذخیره فایل در مسیر مشخص‌شده
            using (var steram = new FileStream(filePath, FileMode.Create)) 
            {
                await file.CopyToAsync(steram);
            }

            return uniqueFileName;
        }
    }
}
