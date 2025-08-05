//using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

/// <summary>
/// Summary description for Startup
/// </summary>
/// 

[assembly: OwinStartup(typeof(Startup))]
public class Startup
{

    public void Configuration(IAppBuilder app)
    {
        //var config = new HubConfiguration(); 
        //config.EnableJSONP = true;
        //config.EnableDetailedErrors = true;        
        //app.MapSignalR(config); 
    }
}