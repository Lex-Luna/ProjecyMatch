using Firebase.Database;
using Firebase.Database.Query;
using ProyecMatch.Conexiones;
using ProyecMatch.Models;
using System;
using System.Threading.Tasks;

public class Dusuario
{
    private string _IdUsuario;

    public async Task<string> CrearUsuarioEnFirebase(Musuario parametros)
    {
        try
        {
            // Asegúrate de que Constantes.firebase es una instancia de FirebaseClient
            var firebase = Constantes.firebase;

            // Crear un nuevo nodo en "Usuario" con ID auto-generado
            var result = await firebase
                .Child("Usuarios")
                .PostAsync(new Musuario()
                {
                    Admin = parametros.Admin,
                    Apellidos = parametros.Apellidos,
                    Password = parametros.Password, // Considera no guardar password en texto plano
                    Correo = parametros.Correo,
                    Estado = true,
                    Nombres = parametros.Nombres,
                    Idusuarios = parametros.Idusuarios,
                    //FechaCreacion = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
                });

            // result.Key contiene el ID auto-generado por Firebase
            _IdUsuario = result.Key;

            // Si quieres usar tu propio ID, actualiza el nodo con ese ID
            if (!string.IsNullOrEmpty(parametros.Idusuarios))
            {
                await firebase
                    .Child("Usuarios")
                    .Child(parametros.Idusuarios)
                    .PutAsync(new Musuario()
                    {
                        Admin = parametros.Admin,
                        Apellidos = parametros.Apellidos,
                        Password = parametros.Password,
                        Correo = parametros.Correo,
                        Estado = true,
                        Nombres = parametros.Nombres,
                        Idusuarios = parametros.Idusuarios
                        //FechaNaciemiento = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
                    });

                _IdUsuario = parametros.Idusuarios;
            }

            Console.WriteLine($"✅ Usuario guardado en Firebase Database. ID: {_IdUsuario}");
            return _IdUsuario;
        }
        catch (FirebaseException firebaseEx)
        {
            Console.WriteLine($"❌ Error de Firebase: {firebaseEx.Message}");
            throw new Exception($"Error de Firebase: {firebaseEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error general: {ex.Message}");
            throw;
        }
    }
}