using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FileBrowsing.WebApi.Models
{
    [DataContract]
    public class FileSystemViewModel
    {
        [DataMember]
        public int SmallFileCounter { get; set; }

        [DataMember]
        public int MediumFileCounter { get; set; }

        [DataMember]
        public int LargeFileCounter { get; set; }
        
        [DataMember]
        public PathModel PathModel { get; set; }

        [DataMember]
        public List<DirectoryModel> Directories { get; set; }

        [DataMember]
        public List<FileModel> Files { get; set; }
    }
}