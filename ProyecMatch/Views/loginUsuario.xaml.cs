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
	public partial class loginUsuario : ContentPage
	{
		public loginUsuario ()
		{
			InitializeComponent ();
            BindingContext = new VMloginUsuario(Navigation);
        }
	}
}