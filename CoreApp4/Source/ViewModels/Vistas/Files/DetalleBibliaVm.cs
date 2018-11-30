using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YllariFM.Source.ViewModels.Vistas.Files
{
    public class DetalleBibliaVm
    {
        public string Fecha { get; set; }

        public DetalleBibliaVm()
        {
            Fecha = DateTime.Now.ToString();
        }
    }
}
