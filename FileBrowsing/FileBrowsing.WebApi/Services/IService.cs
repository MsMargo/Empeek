using FileBrowsing.WebApi.Models;

namespace FileBrowsing.WebApi.Services
{
    public interface IService
    {
        FileSystemViewModel GetFileSystemSnapshotByPath(string path);
    }
}
