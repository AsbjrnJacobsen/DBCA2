using Microsoft.AspNetCore.Mvc;

namespace AmazonKiller2000.Controllers;

[ApiController]
public class DebugController(IHostEnvironment env, IConfiguration configuration) : ControllerBase
{
    [HttpGet("IsOnline")]
    public IActionResult IsOnline()
    {
        return Ok("Online");
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var debugInfo = new Dictionary<string, object>
        {
            { "Environment", env.EnvironmentName },
            { "ApplicationName", env.ApplicationName },
            { "OS", System.Runtime.InteropServices.RuntimeInformation.OSDescription },
            { "Framework", System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription },
            { "ProcessorCount", Environment.ProcessorCount },
            { "Configuration", configuration.AsEnumerable() }
        };

        return Ok(debugInfo);
    }
}