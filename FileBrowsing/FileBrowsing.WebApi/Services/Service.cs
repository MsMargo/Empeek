using FileBrowsing.WebApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileBrowsing.WebApi.Services
{
    public class Service: IService
    {
        private const int bytesInMegabyte = 1048576;

        private FileSystemViewModel GetDrives()
        {
            var viewModel = new FileSystemViewModel();
            var driveNames = DriveInfo.GetDrives().Select(d => d.Name);

            viewModel.Directories = driveNames.Select(x => new DirectoryModel { DirectoryName = x }).ToList();
            viewModel.Files = new List<FileModel>();

            return viewModel;
        }

        public FileSystemViewModel GetFileSystemSnapshotByPath(string path)
        {
            var viewModel = new FileSystemViewModel();

            if (path == null)
            {
                viewModel = GetDrives();
                foreach (var drive in viewModel.Directories)
                {
                    var currentDirectory = new DirectoryInfo(drive.DirectoryName);
                    SortFilesBySize(currentDirectory, ref viewModel);
                }
            }

            if (Directory.Exists(path))
            {
                var currentDirectory = new DirectoryInfo(path);
                SortFilesBySize(currentDirectory, ref viewModel);

                var directoryList = Directory.EnumerateDirectories(path);
                var directoryListModel = new List<DirectoryModel>();
                var fileListModel = new List<FileModel>();

                foreach (var directory in directoryList)
                {
                    var dir = new DirectoryInfo(directory);
                    var dirModel = new DirectoryModel();
                    dirModel.DirectoryName = dir.Name;
                    directoryListModel.Add(dirModel);
                }

                var fileList = Directory.EnumerateFiles(path);

                foreach (var fileItem in fileList)
                {
                    var file = new FileInfo(fileItem);
                    var fileModel = new FileModel();
                    fileModel.FileName = file.Name;
                    fileListModel.Add(fileModel);
                }

                viewModel.Directories = directoryListModel;
                viewModel.Files = fileListModel;
            }
            viewModel.PathModel = InitializePath(path);

            return viewModel;
        }

        private PathModel InitializePath(string currentPath)
        {
            var pathSeparator = "\\";
            var pathModel = new PathModel() { IsRoot = false, CurrentPath = currentPath};

            if(currentPath == null)
            {
                pathModel.IsRoot = true;
                return pathModel;
            }

           var countLevels = currentPath.Split(new string[] { pathSeparator }, StringSplitOptions.None).Length; 

            var indexLastSeparator = currentPath.LastIndexOf(pathSeparator);
            // for the top levelUp Path is absent
            pathModel.LevelUpPath = countLevels == 2 ? String.Empty : currentPath.Substring(0, indexLastSeparator);
            
            return pathModel;
        }

        private void SortFilesBySize(DirectoryInfo directoryInfo, ref FileSystemViewModel viewModel)
        {
            try
            {
                var fileInfoList = directoryInfo.GetFiles();
                foreach (var fileInfo in fileInfoList)
                {
                    var size = fileInfo.Length / bytesInMegabyte;
                    if (size < 10)
                    {
                        viewModel.SmallFileCounter++;
                    }
                    else if (size > 100)
                    {
                        viewModel.LargeFileCounter++;
                    }
                    else
                    {
                        viewModel.MediumFileCounter++;
                    }
                }

                var directoryInfoList = directoryInfo.GetDirectories();

                foreach (var directoryInfoItem in directoryInfoList)
                {
                    SortFilesBySize(directoryInfoItem, ref viewModel);
                }
            }
            catch (Exception)
            {}
        }
    }
}