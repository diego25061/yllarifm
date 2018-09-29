using System;
using System.Collections.Generic;

namespace YllariFM.Models.DB
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Servicio = new HashSet<Servicio>();
        }

        public int IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string TipoProveedor { get; set; }

        public ICollection<Servicio> Servicio { get; set; }
    }
}
