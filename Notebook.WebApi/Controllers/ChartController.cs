using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Notebook.SignalRApi.DataStorage;
using Notebook.SignalRApi.HubConfig;
using Notebook.SignalRApi.TimersFeatures;

namespace Notebook.SignalRApi.Controllers
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
