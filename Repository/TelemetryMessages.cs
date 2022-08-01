using System;
using System.Text;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using System.Threading.Tasks;
using IoTProject.Models;
using Newtonsoft.Json;


namespace IoTProject.Repository
{
    public class TelemetryMessages
    {
        static string connectionstring="HostName=NxTIoTTraining.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=RlHaCMxWGXDfNA+0BPe7b8WPjOnS+7B7EUAfBQSMOzg=";
        static RegistryManager registryManager=RegistryManager.CreateFromConnectionString(connectionstring);

        static DeviceClient client=null; 
        static string deviceconnectionstring="HostName=NxTIoTTraining.azure-devices.net;DeviceId=KavitaIoTDevice;SharedAccessKey=AmI/3Fq6LP+PQtPH9sLCcoUiy5h2V7Z+RGOqYaxbbAs=";
    
    public static async Task SendDeviceToCloudMessageAsync(string devicename)
    {
        try
        {
            client=DeviceClient.CreateFromConnectionString(deviceconnectionstring,Microsoft.Azure.Devices.Client.TransportType.Mqtt);
            var device=await registryManager.GetTwinAsync(devicename);
          
            TwinCollection tproperties;
            tproperties=device.Properties.Reported;
            while (true)
            {
                var telementaryDatapoint= new 
                {
                    temperature = tproperties["temperature"],
                    pressure = tproperties["pressure"],
                    drift = tproperties["drift"],
                    accuracy = tproperties["accuracy"],
                    supplyvoltagelevel= tproperties["supplyvoltagelevel"],
                    fullscale= tproperties["fullscale"],
                    frequency= tproperties["frequency"],
                    sensortype= tproperties["sensortype"]
                   
                };

                string messagestring="";
                messagestring=JsonConvert.SerializeObject(telementaryDatapoint);
                var message=new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(messagestring));

                await client.SendEventAsync(message);
                Console.WriteLine("{0} > Sending message : {1}",DateTime.Now,messagestring);
                await Task.Delay(1000*10);

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
    }
}