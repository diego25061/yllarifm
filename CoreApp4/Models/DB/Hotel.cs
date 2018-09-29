using System;
using System.Collections.Generic;

namespace YllariFM.Models.DB
{
    public partial class Hotel
    {
        public string Nombre { get; set; }
        public int IdHotel { get; set; }
        public int Estrellas { get; set; }
    }
}
