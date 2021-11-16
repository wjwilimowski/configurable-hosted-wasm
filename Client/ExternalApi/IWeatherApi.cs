using System.Collections.Generic;
using System.Threading.Tasks;
using hosted_wasm.Shared;
using Refit;

namespace hosted_wasm.Client.ExternalApi
{
	public interface IWeatherApi
	{
		[Get("WeatherForecast")]
		Task<List<WeatherForecast>> GetWeatherForecastsAsync();
	}
}