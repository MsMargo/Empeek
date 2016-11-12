using System.Runtime.Serialization;

namespace FileBrowsing.WebApi.Models
{
    [DataContract]
    public class PathModel
    {
        [DataMember]
        public string CurrentPath { get; set; }

        [DataMember]
        public string LevelUpPath { get; set; }

        [DataMember]
        public bool IsRoot { get; set; }
    }
}