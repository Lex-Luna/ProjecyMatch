using ProyecMatch.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyecMatch
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new CrearCuenta());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
