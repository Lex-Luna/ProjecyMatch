using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyecMatch.Models
{
    public class Musuario
    {
        public string Idusuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public string PaisActual { get; set; }
        public string CiudadActual { get; set; }
        public string DescripcionPersonaje { get; set; }

        // Cambiar a nullable bool
        public bool? Estado { get; set; } = true;
        public bool? Admin { get; set; } = false;

        // Cambiar a nullable DateTime
        public DateTime? FechaNaciemiento { get; set; }

        // Cambiar a nullable int
        public int? PuntosTotales { get; set; } = 0;

        // Propiedades adicionales para binding si es necesario
        [JsonIgnore]
        public bool EstadoBool => Estado ?? true;

        [JsonIgnore]
        public int PuntosTotalesInt => PuntosTotales ?? 0;
    }
}

