namespace NewsApp.Services.Files
{
    public class FilesService : IFilesService
    {
        public async Task UploadAsync(string path,IFormFile file)
        {
            path = Path.Combine(path, file.FileName);
            if (file.Length > 0)
            {
                using (Stream stream = new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
        }
    }
}
