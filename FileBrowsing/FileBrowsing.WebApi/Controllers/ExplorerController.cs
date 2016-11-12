using FileBrowsing.WebApi.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FileBrowsing.WebApi.Controllers
{
    [EnableCors("http://localhost:57798", "*", "*")]
    public class ExplorerController : ApiController
    {
        private IService _service { get; set; }

        private JsonSerializerSettings jsonSettings { get; set; }

        public ExplorerController(IService service)
        {
            _service = service;

            jsonSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        public string Get(string path)
        {
            var fileSystemSnapshot = _service.GetFileSystemSnapshotByPath(path);

            return JsonConvert.SerializeObject(fileSystemSnapshot, jsonSettings);
        }
    }
}
