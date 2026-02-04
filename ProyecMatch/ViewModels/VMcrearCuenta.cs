using Firebase.Auth;
using Newtonsoft.Json;
using ProyecMatch.Conexiones;
using ProyecMatch.Datos;
using ProyecMatch.Models;
using ProyecMatch.Views;
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
        string txtDescripPersonaje;

        #endregion
        #region OBJETOS 
        public string TxtNombre { get { return txtNombre; } set { SetValue(ref txtNombre, value); } }
        public string TxtApellido { get { return txtApellido; } set { SetValue(ref txtApellido, value); } }
        public string TxtCorreo { get { return txtCorreo; } set { SetValue(ref txtCorreo, value); } }
        public string TxtPassword { get { return txtPassword; } set { SetValue(ref txtPassword, value); } }
        public string TxtDescripPersonaje { get { return txtDescripPersonaje; } set { SetValue(ref txtDescripPersonaje, value); } }
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
                    if (TxtPassword.Length < 6)
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

        public async Task<string> ObtenerIdUsuario()
        {

            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constantes.WebapyFirebase));
                var guardarId = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                var refrescarCOntenido = await authProvider.RefreshAuthAsync(guardarId);
                /*Esete es el token que se guarda en el Usuario*/
                Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(refrescarCOntenido));
                _IdUsuario = guardarId.User.LocalId;
                //Preferences.Remove("MyFirebaseRefreshToken");

            }
            catch (Exception)
            {
                await DisplayAlert("Alerta", "X tu seguridad la sesion se a cerrado", "Ok");

            }
            return _IdUsuario;
        }
        public async Task InsertarUsuario()
        {
            try
            {
                var funcion = new Dusuario();
                var parametros = new Musuario();
                parametros.Estado = true;
                parametros.Correo = TxtCorreo;
                parametros.Nombre = TxtNombre;
                parametros.Apellido = TxtApellido;
                parametros.PaisActual = TxtPaisActual;
                parametros.CiudadActual = TxtCiudadActual;
                parametros.Password = TxtPassword;
                parametros.DescripcionPersonaje = TxtDescripPersonaje;
                parametros.PuntosTotales = 0;
                parametros.Idusuario = _IdUsuario;
                await funcion.InserUsuario(parametros);
                //_IdUsuario = await funcion.InserUsuario(parametros);
            }
            catch (Exception er)
            {
                //await DisplayAlert("Alerta", "no se pudo crear el usuario", "Ok" + er);
                throw er;
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
                                await ObtenerIdUsuario();
                                await InsertarUsuario();
                                await DisplayAlert("Perfecto", "Se a creado tu cuenta exitosamente", "Ok");
                                await Navigation.PushAsync(new loginUsuario());

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
