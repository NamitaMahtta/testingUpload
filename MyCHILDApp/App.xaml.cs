using Xamarin.Forms;

namespace MyCHILDApp
{
    public partial class App : Application
    {
        public static bool IsUserLoggedIn { get; set; }
        public App()
        {
            InitializeComponent();

            //MainPage = new MyCHILDAppPage();
            //MainPage = new NavigationPage(new MyCHILDAppPage()); //chnaged to Navigation Page because the MultiList is a different page

            //MainPage = new MainPage(); //For the tabbed page to appear first

            /*if (!IsUserLoggedIn) {*/
                MainPage = new NavigationPage(new MainPage());
            /*} else{
                MainPage = new NavigationPage(new MyCHILDApp.MyCHILDAppPage());
            }*/
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
