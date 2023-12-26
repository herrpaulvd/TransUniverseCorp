using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class ServiceAddress
    {
        public const string SpaceRoute = "https://localhost:7066"; // TODO: switch to https
        public const string Customer = "https://localhost:7154";
        public const string Driver = "https://localhost:7130";
        public const string User = "https://localhost:7159";
        public const string IdentityServer = "https://localhost:7202";

        public const string IdentitySecret = "superpupersecurepassword228";
    }
}
