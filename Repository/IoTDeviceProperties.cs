using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using System.Threading.Tasks;
using IoTProject.Models;

namespace IoTProject.Repository
{
    public class IoTDeviceProperties
    {
         static string connectionstring="HostName=NxTIoTTraining.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=RlHaCMxWGXDfNA+0BPe7b8WPjOnS+7B7EUAfBQSMOzg=";
         static RegistryManager registryManager=RegistryManager.CreateFromConnectionString(connectionstring);
        
         static DeviceClient client= null;
         static string deviceconnectionstring="HostName=NxTIoTTraining.azure-devices.net;DeviceId=KavitaIoTDevice;SharedAccessKey=AmI/3Fq6LP+PQtPH9sLCcoUiy5h2V7Z+RGOqYaxbbAs=";

         public static async Task UpdateReportedProperties(string devicename,ReportedProperties properties)
         {
            client= DeviceClient.CreateFromConnectionString(deviceconnectionstring,Microsoft.Azure.Devices.Client.TransportType.Mqtt);
            TwinCollection reportedproperties, connectivity;
            reportedproperties=new TwinCollection();
            connectivity=new TwinCollection();
            connectivity["type"]="cellular";
            reportedproperties["connectivity"]=connectivity;
            reportedproperties["temperature"]=properties.temperature;
            reportedproperties["pressure"]=properties.pressure;
            reportedproperties["drift"]=properties.drift;
            reportedproperties["accuracy"]=properties.accuracy;
            reportedproperties["supplyvoltagelevel"]=properties.supplyvoltagelevel;
            reportedproperties["fullscale"]=properties.fullscale;
            reportedproperties["frequency"]=properties.frequency;
            reportedproperties["sensortype"]=properties.sensortype;
            reportedproperties["DateTimeLastAppLaunch"]=properties.DateTimeLastAppLaunch;

            await client.UpdateReportedPropertiesAsync(reportedproperties);
         }

        public static async Task UpdateDesiredProperties(string devicename)
        {
            client=DeviceClient.CreateFromConnectionString(deviceconnectionstring,Microsoft.Azure.Devices.Client.TransportType.Mqtt);
            var device=await registryManager.GetTwinAsync(devicename);
            TwinCollection desiredproperties,telemetryconfig;
            desiredproperties=new TwinCollection();
            telemetryconfig=new TwinCollection();
            telemetryconfig["frequency"]="sHz";
            desiredproperties["telemetryconfig"]=telemetryconfig;
            device.Properties.Desired["telemetryconfig"]=telemetryconfig;
            await registryManager.UpdateTwinAsync(device.DeviceId,device,device.ETag);

        }

        public static async Task<Twin> GetDevicePropertiesAsync(string deviceid)
        {
            var device =await registryManager.GetTwinAsync(deviceid);
            return device;
        }

        public static async Task AddTagsandQuery(string deviceid)
        {
            var device=await registryManager.GetTwinAsync(deviceid);
            var patch=
            @"{
                tags:{
                    location:{
                        region:'US',
                        plant:'IotDevice'
                    }
                }
            }";
            await registryManager.UpdateTwinAsync(device.DeviceId,patch,device.ETag);
        }
    }
}