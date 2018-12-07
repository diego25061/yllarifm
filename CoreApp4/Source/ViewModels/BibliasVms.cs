using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YllariFM.Source.ViewModels {
    public class BibliasVms {

        public class BibliaDto {
            public int IdBiblia { get; set; }
            public int Mes { get; set; }
            public int Anho { get; set; }
        }

        public class LeerIndexBibliaDto {
            public int IdBiblia { get; set; }
            public string Mes { get; set; }
            public int Anho { get; set; }
        }
    }
}
