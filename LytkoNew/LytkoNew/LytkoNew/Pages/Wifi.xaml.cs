using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;



namespace LytkoNew.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Wifi : ContentPage
    {
        public Wifi()
        {
            InitializeComponent();

            int nameCount = 10;
            string connected = "Сеть 5";

            TapGestureRecognizer PopupOpen = new TapGestureRecognizer();
            PopupOpen.Tapped += async (s, e) => PopupOpenHandler(s, e);


            for (int i = 0; i < nameCount; i++)
            {
                Label[] names = new Label[nameCount]; 

                names[i] = new Label
                {
                    Text = "Сеть " + i.ToString(),
                    TextColor = Color.FromHex("#fff"),
                    FontSize = 18,
                };

                names[i].GestureRecognizers.Add(PopupOpen);
                namesContainer.Children.Add(names[i]);

                if (names[i].Text == connected)
                   names[i].TextColor = Color.FromHex("#27AFE9");
                
            }



            

        }

        async private void PopupOpenHandler(object sender, EventArgs e)
        {
            Label clickedName = (Label)sender;
            await DisplayPromptAsync(clickedName.Text, "Password");
        }

        
    }
}