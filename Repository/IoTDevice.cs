
using Microsoft.Azure.Devices;
using System.Threading.Tasks;

namespace IoTProject.Repository
{
    public class IoTDevice
    {
        static RegistryManager registryManager;
        static string connectionstring="HostName=NxTIoTTraining.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=RlHaCMxWGXDfNA+0BPe7b8WPjOnS+7B7EUAfBQSMOzg=";
        static Device device;

        public static async Task<Device> AddDeviceAsync(string deviceid)
        {
            registryManager=RegistryManager.CreateFromConnectionString(connectionstring);
            device=await registryManager.AddDeviceAsync(new Device(deviceid));
            return device;

        }

          public static async Task<Device> GetDeviceAsync(string deviceid)
        {
            registryManager=RegistryManager.CreateFromConnectionString(connectionstring);
            device=await registryManager.GetDeviceAsync(deviceid);
            return device;

        }
        
          public static async Task UpateDeviceAsync(string deviceid)
        {
            Device updevice;
            registryManager=RegistryManager.CreateFromConnectionString(connectionstring);
            updevice=await registryManager.GetDeviceAsync(deviceid);
            updevice.Status=DeviceStatus.Enabled;
            await registryManager.UpdateDeviceAsync(updevice);
           
        }
        
          public static async Task RemoveDeviceAsync(string deviceid)
        {
            registryManager=RegistryManager.CreateFromConnectionString(connectionstring);
            await registryManager.RemoveDeviceAsync(deviceid);
           
        }
        
    }
}