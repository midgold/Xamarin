using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

using uPLibrary.Networking.M2Mqtt;
using System.Text;
using uPLibrary.Networking.M2Mqtt.Messages;
using LytkoNew.Models;
using LytkoNew.Pages;

using System.ComponentModel;

namespace LytkoNew
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {   
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            Room room = new Room()
            {
                Title = "",
                IconImageSource = "room.png"
            };

            Page settings = new Settings()
            {
                Title = "",
                IconImageSource = "settings.png"
            };
            Page addDevice = new AddDevice()
            {
                Title = "",
                IconImageSource = "addDevice.png",
                BackgroundColor = Color.FromHex("#282828")
            };
            
            Page message = new Message()
            {
                Title = "",
                IconImageSource = "message.png"
            };
            Children.Add(room);
            Children.Add(settings);
            Children.Add(addDevice);
            //this.Children.Add(message);

            BarBackgroundColor = Color.FromHex("#2f2f2f");
            SelectedTabColor = Color.FromHex("#27AFE9");
            UnselectedTabColor = Color.FromHex("#ffffff");

            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }
    }
}