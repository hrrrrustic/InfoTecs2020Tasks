using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Task2
{//<textarea @bind-value="ReaderSettings.SourceLink" @bind-value:event="oninput" />
    //<input type="checkbox" @bind-value="ReaderSettings.SupportFormatting" />
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}