using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YllariFM.Models.DB;

namespace YllariFM.Source.ViewModels.Api {

    public class ServServDto {
        public int IdServicio;
        public string Fecha;
        public string Ciudad;
        public string Servicio;
        public string Hotel;
        public int Pasajeros;
        public string NombrePasajero;
        //public int idAgencia;
        public string Tren;
        public string Alm;
        public string Obs;
    }

    public class ServTransporteDto {
        public int IdServicio;
        public string Fecha;
        public string Ciudad;
        public string HoraRecojo;
        public string HoraSalida;
        public string Vuelo;
        public string Servicio;
        public int Pasajeros;
        public string NombrePasajero;
        public string Vr;
        public string Tc;
        public string Transp;
        public string Obs;
        public string Hotel;
    }

    public class FileDto {
        public string Codigo { get; set; }
        public int IdBiblia { get; set; }
        public string Descripcion { get; set; }
        public int IdAgencia { get; set; }
        public string FechaCreacion { get; set; }
        public List<ServServDto> Servicios;
        public List<ServTransporteDto> Transportes;

        public static FileDto GenerarDto(File file) {
            FileDto dto = new FileDto {
                Codigo = file.Codigo,
                Descripcion = file.Descripcion,
                IdBiblia = file.IdBiblia,
                IdAgencia = file.IdAgencia,
                FechaCreacion = Utils.formatoFecha(file.FechaCreacion),
                Servicios = new List<ServServDto>(),
                Transportes = new List<ServTransporteDto>()
            };

            foreach (var s in file.Servicio) {
                if (s.TipoServicio == Constantes.TipoServicio.Servicio)
                    dto.Servicios.Add(new ServServDto() {
                        IdServicio = s.IdServicio,
                        Fecha = Utils.datetimeAString(s.Fecha),
                        Ciudad = s.Ciudad,
                        Servicio = s.Nombre,
                        //TODO AGREGAR HOTEEEEEEL CTM
                        //hotel = s.Hotel,
                        Pasajeros = s.Pasajeros,
                        NombrePasajero = s.NombrePasajero,
                        Tren = s.Tren,
                        Alm = s.Alm,
                        Obs = s.Observaciones
                    });
                else
                if (s.TipoServicio == Constantes.TipoServicio.Transporte)
                    dto.Transportes.Add(new ServTransporteDto() {
                        IdServicio = s.IdServicio,
                        Fecha = Utils.datetimeAString(s.Fecha),
                        Ciudad = s.Ciudad,
                        Servicio = s.Nombre,
                        HoraRecojo = Utils.timespanAStringHora(s.HoraRecojo.Value),
                        HoraSalida = Utils.timespanAStringHora(s.HoraSalida.Value),

                        Vuelo = s.Vuelo,
                        Pasajeros = s.Pasajeros,
                        NombrePasajero = s.NombrePasajero,
                        Vr = s.Vr,
                        Transp = s.Transporte,
                        Obs = s.Observaciones,
                        Tc = s.Tc
                    });
            }
            return dto;
        }

        public static File GenerarDbo(FileDto dto) {
            File file = new File();
            file.Codigo = dto.Codigo;
            file.IdBiblia = dto.IdBiblia;
            file.Descripcion = dto.Descripcion;
            file.IdAgencia = dto.IdAgencia;
            file.FechaCreacion = DateTime.Now;
            List<Servicio> servs = new List<Servicio>();

            foreach (var se in dto.Servicios) {
                var dbServ = new Servicio() {
                    IdServicio = se.IdServicio,
                    Fecha = Utils.stringFechaADatetime(se.Fecha),
                    TipoServicio = Constantes.TipoServicio.Servicio,
                    Nombre = se.Servicio,
                    Ciudad = se.Ciudad,
                    Hotel = se.Hotel,
                    Pasajeros = se.Pasajeros,
                    NombrePasajero = se.NombrePasajero,
                    Tren = se.Tren,
                    Alm = se.Alm,
                    Observaciones = se.Obs
                };
                servs.Add(dbServ);
            }
            foreach (var trans in dto.Transportes) {
                var s = new Servicio() {
                    IdServicio = trans.IdServicio,
                    Fecha = Utils.stringFechaADatetime(trans.Fecha),
                    TipoServicio = Constantes.TipoServicio.Transporte,
                    Ciudad = trans.Ciudad,
                    HoraRecojo = Utils.stringHoraATime(trans.HoraRecojo),
                    HoraSalida = Utils.stringHoraATime(trans.HoraSalida),
                    Vuelo = trans.Vuelo,
                    Nombre = trans.Servicio,
                    NombrePasajero = trans.NombrePasajero,
                    Vr = trans.Vr,
                    Transporte = trans.Transp,
                    Observaciones = trans.Obs,
                    Pasajeros = trans.Pasajeros,
                    Tc = trans.Tc
                };
                servs.Add(s);
            }
            file.Servicio = servs;
            return file;
        }

        public static List<File> GenerarDbos(List<FileDto> dtos) {
            List<File> files = new List<File>();
            foreach (var v in dtos)
                files.Add(GenerarDbo(v));
            return files;
        }

        public static List<FileDto> GenerarDtos(List<File> files) {
            List<FileDto> dtos = new List<FileDto>();
            foreach (var f in files) {
                dtos.Add(GenerarDto(f));
            }
            return dtos;
        }

        public static void ActualizarDbo2(File actualizado, File nuevo) {

            actualizado.Codigo = nuevo.Codigo;
            actualizado.IdBiblia = nuevo.IdBiblia;
            actualizado.Descripcion = nuevo.Descripcion;
            actualizado.IdAgencia = nuevo.IdAgencia;
            actualizado.Servicio = nuevo.Servicio;
        }
        public static void ActualizarDbo(File actualizado, File nuevo) {


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

        }
    }
}