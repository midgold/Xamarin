using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace LytkoNew.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddDevice : ContentPage
    {
        public AddDevice()
        {
            InitializeComponent();
        }

        private async void QRScan(object sender, EventArgs e)
        {
            var scan = new ZXingScannerPage();

            await Navigation.PushAsync(scan);
            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopToRootAsync();
                });
            };

        }
    }
}