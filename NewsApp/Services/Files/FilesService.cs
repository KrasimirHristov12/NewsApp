namespace NewsApp.Services.Files
{
    public class FilesService : IFilesService
    {
        public async Task UploadAsync(string path,IEnumerable<IFormFile> files)
        {
            string initialPath = path;
            foreach (var file in files)
            {

                path = Path.Combine(initialPath, file.FileName);
                if (file.Length > 0)
                {
                    using (Stream stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
        }
    }
}
