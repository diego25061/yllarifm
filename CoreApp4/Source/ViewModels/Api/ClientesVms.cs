using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YllariFM.Models.DB;

namespace YllariFM.Source.ViewModels.Api {
    public class ClienteDto {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string CorreoContacto { get; set; }
        public string NumeroContacto { get; set; }
        public string NumeroAdicional { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }

        public static ClienteDto generarDto(Cliente dbo) {
            ClienteDto c = new ClienteDto();
            c.IdCliente = dbo.IdCliente;
            c.Nombre = dbo.Nombre;
            c.Tipo = dbo.Tipo;
            c.CorreoContacto = dbo.CorreoContacto;
            c.NumeroContacto = dbo.NumeroContacto;
            c.NumeroAdicional = dbo.NumeroAdicional;
            c.Pais = dbo.Pais;
            c.Ciudad = dbo.Ciudad;
            return c;
        }

        public static Cliente generarDbo(ClienteDto dto) {
            Cliente c = new Cliente();
            c.IdCliente = dto.IdCliente;
            c.Nombre = dto.Nombre;
            c.Tipo = dto.Tipo;
            c.CorreoContacto = dto.CorreoContacto;
            c.NumeroContacto = dto.NumeroContacto;
            c.NumeroAdicional = dto.NumeroAdicional;
            c.Pais = dto.Pais;
            c.Ciudad = dto.Ciudad;
            return c;
        }
    }
}
