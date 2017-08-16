using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Web_Tic_tac_toe.StartupOwin))]

namespace Web_Tic_tac_toe
{
    public class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
