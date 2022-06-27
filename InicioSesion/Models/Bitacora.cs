using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace InicioSesion.Models
{
    public partial class Bitacora
    {
        public int Id { get; set; }
        public string Correo { get; set; }
        public string Observaciones { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
