using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LytkoNew.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
        }

        async private void WifiOpen(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pages.Wifi());
        }

        private void AccountOpen(object sender, EventArgs e)
        {

        }

        private void FeedbackOpen(object sender, EventArgs e)
        {

        }

        private void UpdateOpen(object sender, EventArgs e)
        {

        }

        private void TimeOpen(object sender, EventArgs e)
        {

        }
    }
}