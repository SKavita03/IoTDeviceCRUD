using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IoTProject.Repository;



namespace IoTProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IoTDeviceController : ControllerBase
    {

        [HttpPost]
        [Route("AddDevice")]
        public async Task AddDevice(string deviceid)
        {
            await IoTDevice.AddDeviceAsync(deviceid);
        }

        [HttpGet]
        [Route("GetDevice/{deviceid}")]
        public async Task GetDevice(string deviceid)
        {
            await IoTDevice.GetDeviceAsync(deviceid);
        }

        [HttpPut]
        [Route("UpdateDevice/{deviceid}")]
        public async Task UpdateDevice(string deviceid)
        {
            await IoTDevice.UpateDeviceAsync(deviceid);
        }

        [HttpDelete]
        [Route("RemoveDevice/{deviceid}")]
        public async Task RemoveDevice(string deviceid)
        {
            await IoTDevice.RemoveDeviceAsync(deviceid);
        }

    }
}