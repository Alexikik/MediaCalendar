using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(MediaCalendar.IdentityHostingStartup))]

namespace MediaCalendar
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}
