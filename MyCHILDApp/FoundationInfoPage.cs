using System;

using Xamarin.Forms;

namespace MyCHILDApp
{
    public class FoundationInfoPage : ContentPage
    {
        public FoundationInfoPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Any further info page." }
                }
            };
        }
    }
}

