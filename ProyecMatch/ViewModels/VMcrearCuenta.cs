using Firebase.Auth;
using Newtonsoft.Json;
using ProyecMatch.Conexiones;
using ProyecMatch.Datos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProyecMatch.ViewModels
{
    public class VMcrearCuenta: BaseViewModel
    {
        public VMcrearCuenta(INavigation navigation)
        {
            Navigation = navigation;
            /*CerrarSecion();*/
        }
        #region VARIABLES
        string _IdUsuario;
        string estado;
        string txtCorreo;
        string txtNombre;
        string txtApellido;
        string txtPaisActual;
        string txtCiudadActual;
        string txtPassword;

        #endregion
        #region OBJETOS 
        public string TxtNombre { get { return txtNombre; } set { SetValue(ref txtNombre, value); } }
        public string TxtApellido { get { return txtApellido; } set { SetValue(ref txtApellido, value); } }
        public string TxtCorreo { get { return txtCorreo; } set { SetValue(ref txtCorreo, value); } }
        public string TxtPassword { get { return txtPassword; } set { SetValue(ref txtPassword, value); } }
        public string TxtPaisActual { get { return txtPaisActual; } set { SetValue(ref txtPaisActual, value); } }
        public string TxtCiudadActual { get { return txtCiudadActual; } set { SetValue(ref txtCiudadActual, value); } }

        #endregion
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public async Task CrearCuenta()
        {
            {
                try
                {
                    // Validar formato de correo
                    if (!IsValidEmail(TxtCorreo))
                    {
                        await DisplayAlert("Error", "Por favor ingrese un correo electrónico válido", "OK");
                        return;
                    }
                    // Validar longitud de contraseña
                    if (TxtPassword.Length < 5)
                    {
                        await DisplayAlert("Error", "La contraseña debe tener al menos 6 caracteres", "OK");
                        return;
                    }

                    var funcion = new DcrearCuenta();
                    await funcion.CrearCuenta(TxtCorreo, TxtPassword);
                }
                catch (FirebaseAuthException ex)
                {
                    switch (ex.Reason)
                    {
                        case AuthErrorReason.EmailExists:
                            await DisplayAlert("Error", "Este correo ya está registrado", "OK");
                            break;
                        case AuthErrorReason.WeakPassword:
                            await DisplayAlert("Error", "La contraseña es muy débil", "OK");
                            break;
                        default:
                            await DisplayAlert("Error", "Error al crear la cuenta: " + ex.Message, "OK");
                            break;
                    }
                    throw; // Re-lanzar la excepción si necesitas manejarla en un nivel superior
                }
            }
        }

        public async Task ValidCuenta(string correo, string pass)
        {
            /*try
            {*/
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constantes.WebapyFirebase));
            var auth = await authProvider.SignInWithEmailAndPasswordAsync(correo, pass);
            /*vamos a generar un nuevo token serializado*/
            var serializartoken = JsonConvert.SerializeObject(auth);
            Preferences.Set("MyFirebaseRefreshToken", serializartoken);
            var guardarId = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
            //await App.Current.MainPage
            var refrescarCOntenido = await authProvider.RefreshAuthAsync(guardarId);
            Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(refrescarCOntenido));


            /*Correo = guardarId.User.Email;
            var f = new UsuarioD();
            var p = new UsuarioM();
            p.Correo = Correo;
            var data = await f.MostUsuarioXcorreo(p);
            Admin = data[0].Admin;
            if (Admin == false)
            {
                Application.Current.MainPage = new NavigationPage(new Contenedor());
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(new MenuAdmin());
            }

        }
        catch (Exception er)
        {
            throw er;
        }*/





        }
        public async Task btnCrearcuenta()
        {

            try
            {

                if (!string.IsNullOrEmpty(TxtNombre))
                {
                    if (!string.IsNullOrEmpty(TxtApellido))
                    {
                        if (!string.IsNullOrEmpty(TxtCorreo))
                        {
                            if (!string.IsNullOrEmpty(TxtPassword))
                            {

                                await CrearCuenta();
                                //await ObtenerIdUsuario();
                                //await InsertarUsuario();
                                
                            }
                            else
                                await DisplayAlert("Alerta", "Agregue una contraseña", "OK");
                        }
                        else
                            await DisplayAlert("Alerta", "Agregue un correo", "OK");
                    }
                    else
                        await DisplayAlert("Alerta", "Agregue un apellido", "OK");

                }
                else
                    await DisplayAlert("Alerta", "Agregue un nombre", "OK");

            }
            catch (Exception e)
            {

                throw e;
            }




        }
        public ICommand btnCrearcuentaComand => new Command(async () => await btnCrearcuenta());

    }
}
