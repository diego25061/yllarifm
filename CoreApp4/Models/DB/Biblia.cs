using System;
using System.Collections.Generic;

namespace CoreApp4.Models.DB
{
    public partial class Biblia
    {
        public Biblia()
        {
            File = new HashSet<File>();
        }

        public int IdBiblia { get; set; }
        public int Mes { get; set; }
        public int Anho { get; set; }

        public ICollection<File> File { get; set; }
    }
}
