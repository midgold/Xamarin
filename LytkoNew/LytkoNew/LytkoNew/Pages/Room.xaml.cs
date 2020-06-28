using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using LytkoNew.Models;

using uPLibrary.Networking.M2Mqtt;
using System.Text;
using uPLibrary.Networking.M2Mqtt.Messages;

using System.Linq;
using System.Collections.Generic;

namespace LytkoNew.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Room : ContentPage
    {
        DeviceList deviceList = new DeviceList();
        DeviceModel device = new DeviceModel();
        DeviceModel currentDevice = new DeviceModel();
        MqttConfig mqttConfig = new MqttConfig();
        Dictionary<string, DeviceModel> DevicePerId = new Dictionary<string, DeviceModel>();

        public Room()
        {
            InitializeComponent();
            MqttHandler();
        }

        private void MqttHandler()
        {
            mqttConfig.Ip = "lytko.com";
            mqttConfig.Port = 1883;
            mqttConfig.ClientId = "8588605";
            mqttConfig.Username = "Vasya";
            mqttConfig.Password = "vasyapassword";

            MqttClient client = new MqttClient(mqttConfig.Ip, mqttConfig.Port, false, null, null, MqttSslProtocols.None);
            client.Connect(mqttConfig.ClientId, mqttConfig.Username, mqttConfig.Password);

            string[] ssdpTopic = { "Vasya/Ssdp" };
            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE };

            client.Subscribe(ssdpTopic, qosLevels);
            client.MqttMsgPublishReceived += RecivedMessages;

            if (deviceList.AllId != null)
            {
                foreach (string id in deviceList.AllId)
                {
                    string[] deviceTopic = { "Vasya/Lytko/Thermostat/" + id + "/Data" };
                    client.Subscribe(deviceTopic, qosLevels);
                }
            }
            //client.Publish("/Lytko-Thermostat/8588605/set_target_temp", Encoding.UTF8.GetBytes(temp));

        }

        public void RecivedMessages(object sender, MqttMsgPublishEventArgs e)
        {
            SsdpList ssdpList = new SsdpList();
            DeviceData deviceData = new DeviceData();
            DeviceDataModel deviceDataModel = new DeviceDataModel();
            Parsing parsing = new Parsing();


            byte[] currentMessageBytes = e.Message;
            string currentMessage = Encoding.UTF8.GetString(currentMessageBytes, 0, currentMessageBytes.Length);

            string[] splitMessage = currentMessage.Split(':');
            string[] splitBracket = splitMessage[0].Split('{');
            string[] splitQuote = splitBracket[1].Split('"');
            string jsonKey = splitQuote[1];

            if (jsonKey == "ssdp")
            {
                ssdpList = parsing.ParseSsdp(currentMessage);
                deviceList.Count = ssdpList.Ssdp.Count;
                deviceList.AllId = new string[deviceList.Count];

                for (int i = 0; i < ssdpList.Ssdp.Count; i++)
                {
                    deviceList.AllId[i] = ssdpList.Ssdp[i].Id;
                    //DeviceModel currentDevice = new DeviceModel();
                    DevicePerId.Add(deviceList.AllId[i], device);
                }

                GenerateGrid();
                MqttHandler();
            }
            else if (jsonKey == "data")
            {
                deviceData = parsing.ParseDeviceData(currentMessage);
                deviceDataModel = deviceData.Data;

                Frame updateDeviceData = CreateFrameDevice(deviceDataModel.Id);

                device.Id = deviceDataModel.Id;
                device.CurrentTemp = deviceDataModel.Temp;
                device.TargetTemp = deviceDataModel.TargetTemp;

                if (deviceDataModel.ScheduleMode == "1")
                    device.HBtnState = Color.FromHex("#CB3939");
                else
                    device.HBtnState = Color.FromHex("#282828");

                DevicePerId[device.Id] = device;

            }
        }

        private void GenerateGrid()
        {
            Grid grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Star) }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) }
                },
                Margin = new Thickness(8, 0),
                RowSpacing = 8,
                ColumnSpacing = 8
            };
            
            Device.BeginInvokeOnMainThread(() =>
            {
                for (int i = 0; i < deviceList.Count; i++)
                {
                    grid.Children.Add(CreateFrameDevice(deviceList.AllId[i]), i, 0);
                }
                devicesContainer.Content = grid;
            });
        }

        private Frame CreateFrameDevice(string id)
        {
            StackLayout contentContainer = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 0,
                HorizontalOptions = LayoutOptions.Center
            };

            TapGestureRecognizer mkSettingsTap = new TapGestureRecognizer();
            mkSettingsTap.Tapped += (s, e) => {
                MkSettingsOpen(s, e);
            };
            contentContainer.GestureRecognizers.Add(mkSettingsTap);
               
            currentDevice = DevicePerId[id];

            Label hBtn = new Label
            {
                Text = "H",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                TextColor = currentDevice.HBtnState,
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                Margin = new Thickness(0, 10, 0, 0)
            };

            Label currentTemp = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.FromHex("#ffffff"),
                FontSize = 50,
                Text = currentDevice.CurrentTemp
            };

            Image celsius = new Image
            {
                Source = "degree.png"
            };

            Binding bindingHBtn = new Binding { Source = currentDevice, Path = "HBtnState" };
            hBtn.SetBinding(Label.TextColorProperty, bindingHBtn);

            Binding bindingCurrentTemp = new Binding { Source = currentDevice, Path = "CurrentTemp" };
            currentTemp.SetBinding(Label.TextProperty, bindingCurrentTemp);

            contentContainer.Children.Add(hBtn);
            contentContainer.Children.Add(currentTemp);
            contentContainer.Children.Add(celsius);

            Frame deviceFrame = new Frame
            {
                BackgroundColor = Color.FromHex("#2f2f2f"),
                CornerRadius = 8,
                HasShadow = true,
                Padding = new Thickness(0, 6),
                Content = contentContainer
            };

            return deviceFrame;

        }

        async private void MkSettingsOpen(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MkSettings(device.CurrentTemp, device.TargetTemp, device.HBtnState));
        }
    }
}