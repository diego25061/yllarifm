using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YllariFM.Models.DB;

namespace YllariFM.Source.ViewModels.Vistas {

    public class LeerServiciosVm {
        public List<ServicioTotalVm> servsGenerales = new List<ServicioTotalVm>();
        public List<ServicioTotalVm> servsTransporte = new List<ServicioTotalVm>();
    }

    public class ServicioTotalVm {
        public int IdServicio;
        public string Nombre;
        public string Fecha;
        public string Ciudad;
        public string Hotel;
        public string NombrePasajero;
        public string Proveedor;
        public string Tren;
        public string Alm;
        public string Obs;
        public string HoraRecojo;
        public string HoraSalida;
        public string Vuelo;
        public string CantPasajeros;
        public string Vr;
        public string Tc;
        public string Transportista;

        public string CodigoFile;
        public int IdFile;

        public static ServicioTotalVm generarDto(Servicio serv) {
            ServicioTotalVm vm = new ServicioTotalVm();
            vm.CodigoFile = serv.IdFileNavigation.Codigo;
            vm.IdFile = serv.IdFile;

            vm.IdServicio = serv.IdServicio;
            vm.Nombre = serv.Nombre;
            vm.Fecha = Utils.formatoFecha(serv.Fecha);
            vm.Ciudad = serv.Ciudad;
            vm.Hotel = serv.Hotel;
            vm.NombrePasajero = serv.NombrePasajero;
            vm.Proveedor = "No definido";
            if (serv.IdProveedorNavigation != null)
                vm.Proveedor = serv.IdProveedorNavigation.Nombre;
            vm.Tren = serv.Tren;
            vm.Alm = serv.Alm;
            vm.Obs = serv.Observaciones;

            if (serv.HoraRecojo.HasValue)
                vm.HoraRecojo = Utils.timespanAStringHora(serv.HoraRecojo.Value);
            else
                vm.HoraRecojo = "Hora no definida";

            if (serv.HoraSalida.HasValue)
                vm.HoraSalida = Utils.timespanAStringHora(serv.HoraRecojo.Value);
            else
                vm.HoraSalida = "Hora no definida";
            vm.Vuelo = serv.Vuelo;
            vm.CantPasajeros = serv.Pasajeros + (serv.Pasajeros == 1 ? " pasajero" : " pasajeros");
            vm.Vr = serv.Vr;
            vm.Tc = serv.Tc;
            vm.Transportista = "No definido";
            if (serv.IdProveedorNavigation != null)
                vm.Transportista = serv.IdProveedorNavigation.Nombre;
            return vm;
        }
    }

    public class ServicioGeneralVm {
        public int IdServicio;
        public string Nombre;
        public string Fecha;
        public string Ciudad;
        public string Hotel;
        public string Pasajeros;
        public string NombrePasajero;
        public string Proveedor;
        public string Tren;
        public string Alm;
        public string Obs;

        public static ServicioGeneralVm generarDto(Servicio serv) {
            ServicioGeneralVm vm = new ServicioGeneralVm();
            vm.Nombre = serv.Nombre;
            vm.Fecha = Utils.formatoFecha(serv.Fecha);
            vm.Ciudad = serv.Ciudad;
            vm.Hotel = serv.Hotel;
            vm.Pasajeros = serv.Pasajeros + serv.Pasajeros == 1 ? " pasajero" : " pasajeros";
            vm.NombrePasajero = serv.NombrePasajero;
            vm.Proveedor = "No definido";
            if(serv.IdProveedorNavigation!=null)
                vm.Proveedor = serv.IdProveedorNavigation.Nombre;
            vm.Tren = serv.Tren;
            vm.Alm = serv.Alm;
            vm.Obs = serv.Observaciones;

            return vm;
        }
    }

    public class ServicioTransporteVm {
        public string Nombre;
        public string Fecha;
        public string Ciudad;
        public string HoraRecojo;
        public string HoraSalida;
        public string Vuelo;
        public string CantPasajeros;
        public string NombrePasajero;
        public string Vr;
        public string Tc;
        public string Transportista;
        public string Obs;



        public static ServicioTransporteVm generarDto(Servicio serv) {
            ServicioTransporteVm vm = new ServicioTransporteVm();
            vm.Nombre = serv.Nombre;
            vm.Fecha = Utils.formatoFecha(serv.Fecha);
            vm.Ciudad = serv.Ciudad;
            if (serv.HoraRecojo.HasValue)
                vm.HoraRecojo = Utils.timespanAStringHora(serv.HoraRecojo.Value);
            else
                vm.HoraRecojo = "Hora no definida";

            if (serv.HoraSalida.HasValue)
                vm.HoraSalida = Utils.timespanAStringHora(serv.HoraRecojo.Value);
            else
                vm.HoraSalida = "Hora no definida";
            vm.Vuelo = serv.Vuelo;
            vm.CantPasajeros = serv.Pasajeros + serv.Pasajeros == 1 ? " pasajero" : " pasajeros";
            vm.NombrePasajero = serv.NombrePasajero;
            vm.Vr = serv.Vr;
            vm.Tc = serv.Tc;
            vm.Transportista = "No definido";
            if(serv.IdProveedorNavigation!=null)
                vm.Transportista = serv.IdProveedorNavigation.Nombre;
            vm.Obs = serv.Observaciones;
            return vm;
        }
    }

}
