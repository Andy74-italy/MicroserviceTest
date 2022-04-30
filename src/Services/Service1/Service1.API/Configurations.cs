using Microsoft.Extensions.Configuration;
using Service1.Data;

namespace Service1.API.Configurations
{
    public class ConnectionInfo : IDBConnectionInfo
    {
        public IConfiguration Configuration { get; }
        public ConnectionInfo(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        public string ConnectionString => Configuration.GetValue<string>("DatabaseSettings:ConnectionString");

        public string DatabaseName => Configuration.GetValue<string>("DatabaseSettings:DatabaseName");
    }}
