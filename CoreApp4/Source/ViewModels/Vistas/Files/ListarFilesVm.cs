using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YllariFM.Source.ViewModels.Vistas.Files
{
    public class ListarFilesVm
    {

        public class File
        {
            public int Id { get; set; }
            public string Codigo { get; set; }
            public string Descripcion { get; set; }
            public string NombreBiblia { get; set; }
            public string Cliente { get; set; }
            public string FechaCreacion { get; set; }
            public int ServiciosServ { get; set; }
            public int ServiciosTransporte { get; set; }
            public string Estado { get; set; }
        }

        public List<File> Files { get; set; }
    }
}
