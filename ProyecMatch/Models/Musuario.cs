using System;
using System.Collections.Generic;
using System.Text;

namespace ProyecMatch.Models
{
    public class Musuario
    {
        public string Idusuarios { get; set; }
        public bool Admin { get; set; }

        public DateTime FechaNaciemiento { get; set; }
        public bool Estado { get; set; }
        public string Correo { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string PaisActual { get; set; }
        public string CiudadActual { get; set; }
        public string Password { get; set; }
        public int PuntosTotales { get; set; }
    }
}

