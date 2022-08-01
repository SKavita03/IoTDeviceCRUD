using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IoTProject.Models;
using IoTProject.Repository;

namespace IoTProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IotDevicePropertiesController : ControllerBase
    {
        [HttpPut]
        [Route("UpdateReportedProperties")]
        public async Task  UpdateReportedProperties(string devicename,ReportedProperties properties)
        {
            await IoTDeviceProperties.UpdateReportedProperties(devicename,properties);
        }

        [HttpPut]
        [Route("UpdateDesiredProperties")]
        public async Task UpdateDesiredProperties(string devicename)
        {
            await IoTDeviceProperties.UpdateDesiredProperties(devicename);
        }

        [HttpPut]
        [Route("UpdateTags/{deviceid}")]
        public async Task UpdateTags(string deviceid)
        {
            await IoTDeviceProperties.AddTagsandQuery(deviceid);
        }
    }
}