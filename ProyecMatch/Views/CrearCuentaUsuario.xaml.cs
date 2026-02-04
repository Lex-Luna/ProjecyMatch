using ProyecMatch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyecMatch.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearCuenta : ContentPage
    {
        public CrearCuenta()
        {
            InitializeComponent();
            BindingContext = new VMcrearCuenta(Navigation);
        }
    }
}