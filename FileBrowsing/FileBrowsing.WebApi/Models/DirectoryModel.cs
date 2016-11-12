using System.Runtime.Serialization;

namespace FileBrowsing.WebApi.Models
{
    [DataContract]
    public class DirectoryModel
    {
        [DataMember]
        public string DirectoryName { get; set; }
    }
}