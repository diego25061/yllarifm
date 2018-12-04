using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YllariFM.Source.ViewModels.Api {
    public class AgenciaDto {
        public int IdAgencia { get; set; }
        public string Nombre { get; set; }
        public string CorreoContacto { get; set; }
        public string CorreoExtra { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
    }

    public class GrabarAgenciaDto {
        public string Nombre { get; set; }
        public string CorreoContacto { get; set; }
        public string CorreoExtra { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
    }
}
