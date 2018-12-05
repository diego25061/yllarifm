using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YllariFM.Models.DB;
using YllariFM.Source.ViewModels.Api;

namespace YllariFM.Service.Persistence {
    public class FilePerst {
        /*
        public FileDto ActualizarFileCompleto(YllariFmContext _context, File dbFile, FileDto dto) {

            List<int> servsBorrar = new List<int>();
            List<int> servsAgregar = new List<int>();
            List<int> servsActualizar = new List<int>();

            List<int> nuevos = new List<int>();
            List<int> existentes = new List<int>();
            List<int> totales = new List<int>();

            foreach (var ns in dto.Servicios) {
                nuevos.Add(ns.IdServicio);
            }

            foreach(var os in dbFile.Servicio) {
                existentes.Add(os.IdServicio);
            }

            for(int i = 0; i < (nuevos.Count > existentes.Count ? nuevos.Count : existentes.Count); i++) {
                if(!totales.Contains(nuevos[i]))
                    totales.Add(nuevos[i]);
                if (!totales.Contains(existentes[i]))
                    totales.Add(existentes[i]);
            }

            if()

            var obj = FileDto.GenerarDbo(dto);


            actualizado.Codigo = nuevo.Codigo;
            actualizado.IdBiblia = nuevo.IdBiblia;
            actualizado.Descripcion = nuevo.Descripcion;
            actualizado.IdAgencia = nuevo.IdAgencia;

            foreach (var newServ in nuevo.Servicio) {
                Servicio found = null;

                foreach (var antServ in actualizado.Servicio) {
                    if (newServ.IdServicio == antServ.IdServicio) {
                        found = newServ;
                        //---------
                        antServ.Nombre = newServ.Nombre;
                        antServ.HoraRecojo = newServ.HoraRecojo;
                        antServ.HoraSalida = newServ.HoraSalida;
                        antServ.Vuelo = newServ.Vuelo;
                        antServ.Pasajeros = newServ.Pasajeros;
                        antServ.Vr = newServ.Vr;
                        antServ.Tc = newServ.Tc;
                        antServ.IdProveedor = newServ.IdProveedor;
                        antServ.Observaciones = newServ.Observaciones;
                        antServ.Ciudad = newServ.Ciudad;
                        antServ.NombrePasajero = newServ.NombrePasajero;
                        antServ.Tren = newServ.Tren;
                        antServ.Alm = newServ.Alm;
                        antServ.Transporte = newServ.Transporte;
                        antServ.Hotel = newServ.Hotel;
                    }
                }
                if (found == null) {
                    actualizado.Servicio.Add(newServ);
                }
            }


            FileDto.ActualizarDbo(dbFile, obj);


            _context.Update(dbFile);
            _context.SaveChanges();
            var devolucion = FileDto.GenerarDto(dbFile);
            return (devolucion);
        

        }*/
    }
}
