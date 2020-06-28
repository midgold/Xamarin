using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using LytkoNew.Models;

namespace LytkoNew
{
    public class Parsing
    {
        public SsdpList ParseSsdp(string ToParse)
        {
            SsdpList ssdpList = JsonConvert.DeserializeObject<SsdpList>(ToParse);
            return ssdpList;
        }

        public DeviceData ParseDeviceData(string ToParse)
        {
            DeviceData deviceDataModel = JsonConvert.DeserializeObject<DeviceData>(ToParse);
            return deviceDataModel;
        }
    }
}
