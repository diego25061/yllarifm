using System;
using System.Collections.Generic;

namespace YllariFM.Models.DB
{
    public partial class Cliente
    {
        public Cliente()
        {
            File = new HashSet<File>();
        }

        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string CorreoContacto { get; set; }
        public string NumeroContacto { get; set; }
        public string NumeroAdicional { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }

        public ICollection<File> File { get; set; }
    }
}
