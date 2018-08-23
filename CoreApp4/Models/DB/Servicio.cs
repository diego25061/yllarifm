using System;
using System.Collections.Generic;

namespace CoreApp4.Models.DB
{
    public partial class Servicio
    {
        public int IdServicio { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoServicio { get; set; }
        public string Nombre { get; set; }
        public int IdFile { get; set; }
        public bool EsProvincia { get; set; }
        public TimeSpan? HoraRecojo { get; set; }
        public TimeSpan? HoraSalida { get; set; }
        public string Vuelo { get; set; }
        public int Pasajeros { get; set; }
        public string Vr { get; set; }
        public string Tc { get; set; }
        public int? IdProveedor { get; set; }
        public string Observaciones { get; set; }

        public File IdFileNavigation { get; set; }
        public Proveedor IdProveedorNavigation { get; set; }
    }
}
