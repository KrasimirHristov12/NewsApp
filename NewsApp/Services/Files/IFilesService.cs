namespace NewsApp.Services.Files
{
    public interface IFilesService
    {
        Task UploadAsync(string path, IFormFile file);
    }
}
