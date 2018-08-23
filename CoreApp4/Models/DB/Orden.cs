using System;
using System.Collections.Generic;

namespace CoreApp4.Models.DB
{
    public partial class Orden
    {
        public int IdOrden { get; set; }
        public int IdServicio { get; set; }
        public bool? RecordatorioEnviado { get; set; }
        public bool? Rec2Enviado { get; set; }
    }
}
