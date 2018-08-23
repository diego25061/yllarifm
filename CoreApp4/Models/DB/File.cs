using System;
using System.Collections.Generic;

namespace CoreApp4.Models.DB
{
    public partial class File
    {
        public File()
        {
            Servicio = new HashSet<Servicio>();
        }

        public string Codigo { get; set; }
        public int IdFile { get; set; }
        public int IdBiblia { get; set; }
        public string Descripcion { get; set; }
        public int IdAgencia { get; set; }

        public Agencia IdAgenciaNavigation { get; set; }
        public Biblia IdBibliaNavigation { get; set; }
        public ICollection<Servicio> Servicio { get; set; }
    }
}
