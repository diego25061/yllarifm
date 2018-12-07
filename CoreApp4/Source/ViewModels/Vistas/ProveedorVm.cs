using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YllariFM.Models.DB;

namespace YllariFM.Source.ViewModels.Vistas {
    public class ProveedorVm {

        public int IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Correos { get; set; }
        public string NumerosContacto { get; set; }
        public string ServiciosEnCargo { get; set; }

        public static ProveedorVm generarDto(Proveedor pr) {
            ProveedorVm vm = new ProveedorVm();
            vm.IdProveedor = pr.IdProveedor;
            vm.Nombre = pr.Nombre;
            if (pr.TipoProveedor == Constantes.TipoProveedor.Empresa) {
                vm.Tipo = "Empresa";
            }else if(pr.TipoProveedor == Constantes.TipoProveedor.Persona) {
                vm.Tipo = "Persona";
            }//TODO FIX
            vm.Correos = pr.Correo;
            if (!string.IsNullOrEmpty(pr.CorreoAdicional))
                vm.Correos += ", " + pr.CorreoAdicional;
            vm.NumerosContacto = pr.NumeroContacto;
            if ( !string.IsNullOrEmpty(pr.NumeroCntctAdicional) )
                vm.NumerosContacto += ", " + pr.NumeroCntctAdicional;
            vm.ServiciosEnCargo = ""+pr.Servicio.Count;
            return vm;
        }

    }
}
