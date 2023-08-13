using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(Encuestas.App_Start.Startup))]

namespace Encuestas.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(
                    new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions()
                    {
                        AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                        LoginPath = new PathString("/Seguridad/SigIn")
                    });
            app.UseExternalSignInCookie(
                    DefaultAuthenticationTypes.ExternalCookie
                );
        }
    }
}
