using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Hangfire;

[assembly: OwinStartupAttribute("EmailDemoApplication", typeof(EmailDemoApplication.Startup))]

namespace EmailDemoApplication
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

            GlobalConfiguration.Configuration.UseSqlServerStorage("MailDetailContext");

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
