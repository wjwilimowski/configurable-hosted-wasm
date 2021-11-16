using hosted_wasm.Shared;
using Microsoft.AspNetCore.Mvc;

namespace hosted_wasm.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ConfigurationController : ControllerBase
	{
		private readonly FrontendConfiguration _configuration;

		public ConfigurationController(FrontendConfiguration configuration)
		{
			_configuration = configuration;
		}

		[HttpGet]
		public FrontendConfiguration Get()
		{
			return _configuration;
		}
	}
}