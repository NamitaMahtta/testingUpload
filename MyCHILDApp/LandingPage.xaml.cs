using System;
using System.Collections.Generic;
using MyCHILDApp.Model;
using Xamarin.Forms;

namespace MyCHILDApp
{
    public partial class LandingPage : ContentPage
    {
        public LandingPage()
        {
            InitializeComponent();
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var user = new User
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text
            };

            var isValid = AreCredentialsCorrect(user);
            //var isValid = true;
            if (isValid)
            {
                App.IsUserLoggedIn = true;
                //Navigation.InsertPageBefore(new MainPage(), this);
                //Navigation.InsertPageBefore(new MyCHILDAppPage(), this);
                //await Navigation.PopAsync();
                await Navigation.PushAsync(new MyCHILDAppPage());
            }
            else
            {
                messageLabel.Text = "Login failed";
                passwordEntry.Text = string.Empty;
            }
        }

        bool AreCredentialsCorrect(User user){
            if (user.Username.Equals("Test") && user.Password.Equals("test"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
