using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Notebook.WebApi.DataStorage;
using Notebook.WebApi.HubConfig;
using Notebook.WebApi.TimersFeatures;

namespace Notebook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private IHubContext<ChartHub> Hub { get; }

        public ChartController(IHubContext<ChartHub> hub)
        {
            Hub = hub;
        }

        public IActionResult Get()
        {
            var timerManager = new TimerManager(() =>
            {
                Hub.Clients.All.SendAsync("transferchartdata", DataManager.GetData());
            });

            return Ok(new {Message = "Request Completed"});
        }
    }
}
