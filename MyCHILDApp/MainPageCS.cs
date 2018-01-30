using System;
using Xamarin.Forms;

namespace MyCHILDApp
{
    public class MainPageCS : TabbedPage
    {
        public MainPageCS()
        {
            var navigationPage = new NavigationPage(new AboutCMCPage());

            Children.Add(new LandingPage());  //Landing page with Login 
            Children.Add(navigationPage); //Page about general CMC Info
            Children.Add(new AboutPage());  //Page about MyCHILD Tracker
        }
    }
}
