
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YllariFM.Models.DB;

namespace YllariFM.Source.ViewModels
{
    public class CreateFileVm{

        public class ServicioServ{
            public ServicioServ()
            {}
            public string fecha;
            public string ciudad;
            public string servicio;
            public int idHotel;
            public int pasajeros;
            public string nombrePasajero;
            public int idAgencia;
            public string tren;
            public string alm;
            public string obs;
        }

        public class ServicioTransporte{
            public string fecha;

            public string ciudad;
            public string horaRecojo;
            public string horaSalida;
            public string vuelo;
            public string servicio;
            public int pasajeros;
            public string nombrePasajero;
            public string vr;
            public string tc;
            public string transp;
            public string obs;
        }

        //binds
        public string codigo;
        public int idBiblia;
        public int idAgencia;
        public List<ServicioServ> servicios;
        public List<ServicioTransporte> transportes;

        public CreateFileVm(){
            servicios = new List<ServicioServ>();
            transportes = new List<ServicioTransporte>();
        }


        public File toDbFile()
        {
            File file = new File();
            file.Codigo = codigo;
            file.IdBiblia = 1;
            file.IdAgencia = 1;
            file.FechaCreacion = DateTime.Now;

            List<Servicio> servs= new List<Servicio>();
            foreach(var serv in servicios){
                var s = new Servicio()
                {
                    Fecha = Utils.strinFechagADatetime(serv.fecha),
                    TipoServicio = Constantes.TipoServicio.Servicio,
                    Nombre = serv.servicio,
                    Ciudad = serv.ciudad,
                    //EsProvincia = Utils.CiudadEsProvincia(serv.ciudad),
                    Pasajeros = serv.pasajeros,
                    NombrePasajero = serv.nombrePasajero,
                    Tren = serv.tren,
                    Alm = serv.alm,
                    Observaciones = serv.obs
                };
                servs.Add(s);
            }

            foreach (var trans in transportes)
            {
                var s = new Servicio()
                {
                    Fecha = Utils.strinFechagADatetime(trans.fecha),
                    TipoServicio = Constantes.TipoServicio.Transporte,
                    Ciudad = trans.ciudad,
                    HoraRecojo = Utils.stringHoraATime(trans.horaRecojo),
                    HoraSalida = Utils.stringHoraATime(trans.horaSalida),
                    Vuelo = trans.vuelo,
                    Nombre = trans.servicio,
                    NombrePasajero = trans.nombrePasajero,
                    Vr = trans.vr,
                    Transp = trans.transp,
                    Observaciones = trans.obs,
                    //EsProvincia = Utils.CiudadEsProvincia(trans.ciudad),
                    Pasajeros =  trans.pasajeros
                };
                servs.Add(s);
            }
            file.Servicio = servs;
            return file;
        }
    }
}
