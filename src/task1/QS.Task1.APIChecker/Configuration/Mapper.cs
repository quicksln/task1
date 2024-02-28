using QS.Task1.APIChecker.Models;
using QS.Task1.Services.Models;

namespace QS.Task1.APIChecker.Configuration
{
    /// <summary>
    /// Simple mapper class to map between Entry and APIEntry
    /// </summary>
    public static class Mapper
    {
        public static APIEntry Map(Entry entry)
        {
            return new APIEntry
            {
                API = entry.API,
                Description = entry.Description,
                Auth = entry.Auth,
                HTTPS = entry.HTTPS,
                Cors = entry.Cors,
                Link = entry.Link,
                Category = entry.Category,
            };
        }

        public static Entry Map(APIEntry apiEntry)
        {
            return new Entry
            {
                API = apiEntry.API,
                Description = apiEntry.Description,
                Auth = apiEntry.Auth,
                HTTPS = apiEntry.HTTPS,
                Cors = apiEntry.Cors,
                Link = apiEntry.Link,
                Category = apiEntry.Category,
            };
        }
    }
}
