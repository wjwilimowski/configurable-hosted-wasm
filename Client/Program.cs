using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using hosted_wasm.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace hosted_wasm.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            var bootstrapClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
            HttpResponseMessage configResponse = await bootstrapClient.GetAsync("/Configuration");
            string responseContent = await configResponse.Content.ReadAsStringAsync();
            var frontendConfiguration = JsonSerializer.Deserialize<FrontendConfiguration>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Console.WriteLine(frontendConfiguration.ExternalApiUrl);

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(frontendConfiguration.ExternalApiUrl) });

            await builder.Build().RunAsync();
        }
    }
}
