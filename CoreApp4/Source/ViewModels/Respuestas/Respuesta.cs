using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YllariFM.Source.ViewModels
{
    public class Respuesta
    {
        public string msj;
        public string exError;
        public string exTrace;

        public Respuesta(string mensaje) {
            msj = mensaje;
        }

        public Respuesta(string msj, Exception ex) {
            this.msj = msj;
            exError = Utils.GetFullMensajeExcepcion(ex);
            exTrace = ex.StackTrace;
        }
    }
}
