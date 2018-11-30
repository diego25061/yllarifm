using System;
using System.Collections.Generic;

namespace YllariFM.Models.DB
{
    public partial class Hotel
    {
        public int IdHotel { get; set; }
        public string Nombre { get; set; }
        public byte? Estrellas { get; set; }
    }
}
