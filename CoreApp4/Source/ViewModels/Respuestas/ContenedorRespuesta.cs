using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YllariFM.Source.ViewModels
{
    public class ContenedorRespuesta
    {
        public ContenedorRespuesta()
        {

        }

        public ContenedorRespuesta(string contenido, string error)
        {
            this.Contenido = contenido;
            this.MsjError = error;
        }

        public ContenedorRespuesta(string msj, Exception ex) {
            this.Contenido = msj;
            this.MsjError = ex.Message;
        }

        public string Contenido { get; set; }
        public string MsjError { get; set; }

    }
}
