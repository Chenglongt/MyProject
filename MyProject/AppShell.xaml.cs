using MyProject.Pages;
using System.Globalization;

namespace MyProject
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        //如果有更多页面直接添加下面路线
        private readonly static Type[] _routablePageTypes =
            [
                typeof(SigninPage),
                typeof(SignupPage),
                typeof(MyOrdersPage),
                typeof(OrderDetailsPage),
                typeof(DetailsPage),
            ];
        private static void RegisterRoutes() 
        {
            foreach (var pageType in _routablePageTypes) 
            {
                Routing.RegisterRoute(pageType.Name, pageType);
            }
        }

        //FlyoutFooterTapped
        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            await Launcher.OpenAsync("https://www.youtu.com");
        }

        private async void SignoutMenuItem_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.DisplayAlert("Alert", "Signout menu item clicked", "Ok");
        }
    }
}
