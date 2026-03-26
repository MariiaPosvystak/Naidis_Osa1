using Microsoft.Extensions.DependencyInjection;

namespace Naidis_Osa1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var startPage = new StartPage();
            var navPage = new NavigationPage(startPage)
            {
                BarBackgroundColor = Color.FromHex("#50382A"),
                BarTextColor = Color.FromHex("#F5F5F5")
            };
            return new Window(navPage);
        }
    }
}