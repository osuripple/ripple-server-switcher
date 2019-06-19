using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RippleServerSwitcher
{
    public class Pipoli
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private const string endpoint = "https://status.ripple.moe/backend/status_data.php";

        public static async Task<Dictionary<string, ServiceStatus>> FetchServicesStatus()
        {
            using (var result = await httpClient.GetAsync(endpoint))
            {
                string content = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Dictionary<string, ServiceStatus>>(content);
            }
        }
        public static async Task<EmergencyMessage> FetchEmergencyMessage()
        {
            using (var result = await httpClient.GetAsync(endpoint))
            {
                string content = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<EmergencyMessage>(content);
            }
        }
    }

    public class ServiceStatus
    {
        public bool Up;
        public bool Secondary;
    }

    public enum EmergencyMessageType
    {
        Info,
        Warning
    }

    public class EmergencyMessage
    {
        public string Title;
        public string Message;

        [JsonConverter(typeof(StringEnumConverter))]
        public EmergencyMessageType Type;

        public bool Empty { get { return String.IsNullOrEmpty(Message); } }
    }

    public class EmptyEmergencyMessage : EmergencyMessage
    {
        public new bool Empty { get { return true; } }
    }
}
