using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YllariFM.Models.DB;

namespace YllariFM.Source.ViewModels.Vistas {
    public class ListarClienteVm {

        public int IdCliente;
        public string Nombre;
        public string Tipo;
        public string Correos;
        public string NumerosContacto;
        public string Pais;
        public string Ciudad;
        public int filesAsociados;

        public static ListarClienteVm generarDto(Cliente c) {
            ListarClienteVm dto = new ListarClienteVm();
            dto.IdCliente = c.IdCliente;
            dto.Nombre = c.Nombre;
            dto.Tipo = Constantes.TipoCliente.aTexto(c.Tipo);

            if (Utils.stringVacio(c.CorreoContacto))
                dto.Correos = "-";
            else {
                dto.Correos = c.CorreoContacto;
            }
            dto.NumerosContacto = "";
            if (Utils.stringVacio(c.NumeroContacto) ) {
                if (Utils.stringVacio(c.NumeroAdicional)) {
                    dto.NumerosContacto += c.NumeroAdicional;
                }
            } else {
                dto.NumerosContacto = c.NumeroContacto;
                if (!Utils.stringVacio(c.NumeroAdicional)) {
                    dto.NumerosContacto += ", " + c.NumeroAdicional;
                }
            }
            dto.Pais = c.Pais;
            dto.Ciudad = c.Ciudad;
            return dto;
        }

    }
}
