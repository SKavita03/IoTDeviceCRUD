using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IoTProject.Repository;

namespace IoTProject.Controllers
{ 
    [ApiController]
    [Route("[controller]")]
    public class TelemetryMessageController : ControllerBase
    {

     [HttpPut]  
     [Route("SendTelemetryMessage")] 
     public  async Task SendTelemetryMessage(string devicename)
     {
        await TelemetryMessages.SendDeviceToCloudMessageAsync(devicename);
     }  
    }
}