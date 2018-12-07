using System;
using System.Collections.Generic;

namespace YllariFM.Models.DB
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            RegistroRecordatorio = new HashSet<RegistroRecordatorio>();
            Servicio = new HashSet<Servicio>();
        }

        public int IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string TipoProveedor { get; set; }
        public string Correo { get; set; }
        public string NumeroContacto { get; set; }
        public string NumeroCntctAdicional { get; set; }
        public string CorreoAdicional { get; set; }

        public ICollection<RegistroRecordatorio> RegistroRecordatorio { get; set; }
        public ICollection<Servicio> Servicio { get; set; }
    }
}
