using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.FileSystems;

namespace Wa1gon.RigControl
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
#if DEBUG
            appBuilder.UseErrorPage();
#endif
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();

            appBuilder.UseStaticFiles("/files");

            appBuilder.UseFileServer(new FileServerOptions
                {
                    RequestPath = new PathString("/help"),
                    FileSystem = new PhysicalFileSystem(@".\files"),
                    EnableDirectoryBrowsing = true
                });

            // Attribute routing.
            config.MapHttpAttributeRoutes();
            appBuilder.UseWebApi(config);
        } 
    }
}
