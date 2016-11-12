using System.Runtime.Serialization;

namespace FileBrowsing.WebApi.Models
{
    [DataContract]
    public class FileModel
    {
        [DataMember]
        public string FileName { get; set; }
    }
}