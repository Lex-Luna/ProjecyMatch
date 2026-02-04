using Firebase.Database;
using Firebase.Database.Query;
using ProyecMatch.Conexiones;
using ProyecMatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Dusuario
{
    private string _IdUsuario;

    
    public async Task<string> InserUsuario(Musuario parametros)
    {
        try
        {
            var data = await Constantes.firebase
           .Child("Usuarios")
           .PostAsync(new Musuario()
           {
               Admin = parametros.Admin,
               FechaNaciemiento = parametros.FechaNaciemiento,
               Estado = parametros.Estado,
               Correo = parametros.Correo,
               Nombre = parametros.Nombre,
               Apellido = parametros.Apellido,
               PaisActual = parametros.PaisActual,
               CiudadActual = parametros.CiudadActual,
               Password = parametros.Password,
               DescripcionPersonaje = parametros.DescripcionPersonaje,
               PuntosTotales = parametros.PuntosTotales,
               
               
               Idusuario = parametros.Idusuario,
           });
            _IdUsuario = data.Key;
            return _IdUsuario;
        }
        catch (Exception e)
        {

            throw e;
        }

    }

    public async Task<List<Musuario>> MostUsuarioXcorreo(Musuario p)
    {
        var useXcorreo = (await Constantes.firebase
            .Child("Usuarios")
            .OnceAsync<Musuario>())
            .Where(a => a.Object.Correo == p.Correo && a.Object.Estado == true)
            .Select(item => new Musuario
            {
                Idusuario = item.Key,
                Estado = item.Object.Estado,
                Admin = item.Object.Admin,
                Correo = item.Object.Correo,
                Nombre = item.Object.Nombre,
                Apellido = item.Object.Apellido,
                PaisActual = item.Object.PaisActual,
                CiudadActual = item.Object.CiudadActual,
                Password = item.Object.Password,
                DescripcionPersonaje = item.Object.DescripcionPersonaje,
                PuntosTotales = item.Object.PuntosTotales,

                

            }).ToList();
        var json = Newtonsoft.Json.JsonConvert.SerializeObject(useXcorreo);
        return useXcorreo;
    }
}