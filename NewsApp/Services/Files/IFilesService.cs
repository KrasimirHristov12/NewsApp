namespace NewsApp.Services.Files
{
    public interface IFilesService
    {
        Task UploadAsync(string path, IEnumerable<IFormFile> files);
    }
}
