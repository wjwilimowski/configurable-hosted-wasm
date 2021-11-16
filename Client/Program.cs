using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using hosted_wasm.Client.ExternalApi;
using hosted_wasm.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Refit;

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
            var frontendConfiguration = JsonSerializer.Deserialize<FrontendConfiguration>(responseContent);

            builder.Services.AddRefitClient<IWeatherApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(frontendConfiguration!.ExternalApiUrl));

            await builder.Build().RunAsync();
        }
    }
}
