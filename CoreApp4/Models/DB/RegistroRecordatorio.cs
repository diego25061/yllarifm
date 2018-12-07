using System;
using System.Collections.Generic;

namespace YllariFM.Models.DB
{
    public partial class RegistroRecordatorio
    {
        public int IdServicio { get; set; }
        public int IdProveedor { get; set; }
        public DateTime Fecha { get; set; }
        public int IdRegistroRecordatorio { get; set; }

        public Proveedor IdProveedorNavigation { get; set; }
        public Servicio IdServicioNavigation { get; set; }
    }
}
