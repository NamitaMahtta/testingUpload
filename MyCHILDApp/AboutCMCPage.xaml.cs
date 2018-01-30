using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MyCHILDApp
{
    public partial class AboutCMCPage : ContentPage
    {
        public AboutCMCPage()
        {
            InitializeComponent();
        }

        async void OnUpcomingAppointmentsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FoundationInfoPage());
        }
    }
}
