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
    public partial class MkSettings : ContentPage
    {
        bool hClicked;

        public MkSettings(string setCurrentTemp, string setTargetTemp, Color setHBtn)
        {
            InitializeComponent();
            currentTemp.Text = setCurrentTemp;
            targetTemp.Text = setTargetTemp;
            hBtn.TextColor = setHBtn;
        }


        private void hBtnHandler(object sender, EventArgs e)
        {
            if(hClicked)
            {
                hBtn.TextColor = Color.FromHex("#cb3939");
                hClicked = !hClicked;
            }
            else
            {
                hBtn.TextColor = Color.FromHex("#282828");
                hClicked = !hClicked;
            }
        }

        private void tempUp(object sender, EventArgs e)
        {
           
        }

        private void tempDown(object sender, EventArgs e)
        {

        }
        async private void ClosePopup(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}