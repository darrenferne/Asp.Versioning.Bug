using Asp.Versioning.Test.WordApi;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Asp.Versioning.Test.MetadataApi
{
    public class MetadataController : ODataController
    {
        IEnumerable<Metadata> _metadata;

        public MetadataController() 
        {
            _metadata = Enumerable.Empty<Metadata>();
        }
        public IEnumerable<Metadata> Get()
        {
            return _metadata;
        }
    }
}
