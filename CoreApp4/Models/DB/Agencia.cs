using System;
using System.Collections.Generic;

namespace CoreApp4.Models.DB
{
    public partial class Agencia
    {
        public Agencia()
        {
            File = new HashSet<File>();
        }

        public int IdAgencia { get; set; }
        public string Nombre { get; set; }
        public string CorreoContacto { get; set; }
        public string CorreoExtra { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }

        public ICollection<File> File { get; set; }
    }
}
