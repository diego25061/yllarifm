
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YllariFM.Models.DB;

namespace YllariFM.Source.ViewModels
{
    public class CreateFileVm
    {

        public class ServicioServ
        {
            public ServicioServ()
            { }
            public string fecha;
            public string ciudad;
            public string servicio;
            public string hotel;
            public int pasajeros;
            public string nombrePasajero;
            //public int idAgencia;
            public string tren;
            public string alm;
            public string obs;
        }

        public class ServicioTransporte
        {
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
        public string descripcion;
        public List<ServicioServ> servicios;
        public List<ServicioTransporte> transportes;

        public CreateFileVm()
        {
            servicios = new List<ServicioServ>();
            transportes = new List<ServicioTransporte>();
        }


        public File toDbFile()
        {
            File file = new File();
            file.Codigo = codigo;
            file.IdBiblia = idBiblia;
            file.IdCliente = idAgencia;
            file.Descripcion = descripcion;
            file.FechaCreacion = DateTime.Now;

            List<Servicio> servs = new List<Servicio>();
            foreach (var serv in servicios)
            {
                var s = new Servicio()
                {
                    Fecha = Utils.stringFechaADatetime(serv.fecha),
                    TipoServicio = Constantes.TipoServicio.Servicio,
                    Nombre = serv.servicio,
                    Ciudad = serv.ciudad,
                    //Hotel = serv.hotel,
                    //poner hotel
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
                    //Fecha = Utils.stringFechaADatetime(trans.fecha),
                    TipoServicio = Constantes.TipoServicio.Transporte,
                    Ciudad = trans.ciudad,
                    HoraRecojo = Utils.stringHoraATime(trans.horaRecojo),
                    HoraSalida = Utils.stringHoraATime(trans.horaSalida),
                    Vuelo = trans.vuelo,
                    Nombre = trans.servicio,
                    NombrePasajero = trans.nombrePasajero,
                    Vr = trans.vr,
                    Transporte = trans.transp,
                    Observaciones = trans.obs,
                    //EsProvincia = Utils.CiudadEsProvincia(trans.ciudad),
                    Pasajeros = trans.pasajeros,
                    Tc = trans.tc
                };
                servs.Add(s);
            }
            file.Servicio = servs;
            return file;
        }

        public static CreateFileVm fromDb(File dbFile)
        {
            CreateFileVm file = new CreateFileVm();

            file.codigo = dbFile.Codigo;
            file.descripcion = dbFile.Descripcion;
            file.idBiblia = dbFile.IdBiblia;
            file.idAgencia = dbFile.IdCliente;

            //List<ServicioServ> servs = new List<ServicioServ>();
            //List<ServicioTransporte> trans = new List<ServicioTransporte>();

            foreach (var s in dbFile.Servicio)
            {
                if (s.TipoServicio == Constantes.TipoServicio.Servicio)
                    file.servicios.Add(new ServicioServ()
                    {
                        fecha = Utils.datetimeAString(s.Fecha),
                        ciudad = s.Ciudad,
                        servicio = s.Nombre,
                        //TODO AGREGAR HOTEEEEEEL CTM
                        //hotel = s.Hotel,
                        pasajeros = s.Pasajeros,
                        nombrePasajero = s.NombrePasajero,
                        tren = s.Tren,
                        alm = s.Alm,
                        obs = s.Observaciones
                    });
                else
                if (s.TipoServicio == Constantes.TipoServicio.Transporte)
                    file.transportes.Add(new ServicioTransporte()
                    {

                        fecha = Utils.datetimeAString(s.Fecha),
                        ciudad = s.Ciudad,
                        servicio = s.Nombre,
                        horaRecojo = Utils.timespanAStringHora(s.HoraRecojo.Value),
                        horaSalida = Utils.timespanAStringHora(s.HoraSalida.Value),
                        //TODO AGREGAR HOTEEEEEEL CTM
                        //hotel = s.Hotel,
                        vuelo = s.Vuelo,
                        pasajeros = s.Pasajeros,
                        nombrePasajero = s.NombrePasajero,
                        vr = s.Vr,
                        transp = s.Transporte,
                        obs = s.Observaciones,
                        tc = s.Tc
                    });
            }

            return file;
        }

        /*

    public class ServicioServ
    {
        public ServicioServ()
        { }
        public string fecha;
        public string ciudad;
        public string servicio;
        public string hotel;
        public int pasajeros;
        public string nombrePasajero;
        public int idAgencia;
        public string tren;
        public string alm;
        public string obs;
    }

    public class ServicioTransporte
    {
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
         */

    }
}
