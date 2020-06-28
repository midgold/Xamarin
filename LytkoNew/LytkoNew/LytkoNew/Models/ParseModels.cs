using Newtonsoft.Json;
using System.Collections.Generic;

namespace LytkoNew.Models
{
    public class SsdpList
    {
        [JsonProperty("ssdp")]
        public List<SsdpItem> Ssdp { get; set; }
    }
    public class SsdpItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("childrens")]
        public List<GatewayChildrens> Childrens { get; set; }

        [JsonProperty("zoneId")]
        public string ZoneId { get; set; }
    }
    public class GatewayChildrens
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class DeviceData
    {
        [JsonProperty("data")]
        public DeviceDataModel Data { get; set; }
    }

    public class DeviceDataModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("temp")]
        public string Temp { get; set; }

        [JsonProperty("target_temp")]
        public string TargetTemp { get; set; }

        [JsonProperty("schedule_mode")]
        public string ScheduleMode { get; set; }
    }
}
