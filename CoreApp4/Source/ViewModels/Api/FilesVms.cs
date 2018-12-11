using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YllariFM.Models.DB;
using YllariFM.Source.Common;

namespace YllariFM.Source.ViewModels.Api {

    public class ServServDto {
        public int IdServicio;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Definir fecha en servicio")]
        public string Fecha;
        public string Ciudad;
        [Required(ErrorMessage = "Definir nombre en servicio")]
        public string Servicio;
        public string Hotel;
        [Required(ErrorMessage = "Definir cantidad de pasajeros en servicio")]
        public int Pasajeros;
        [Required(ErrorMessage = "Definir nombre de pasajero en servicio")]
        public string NombrePasajero;
        //public int idAgencia;
        public string Tren;
        public string Alm;
        public string Obs;
        public ServServDto() {

        }
    }

    public class ServTransporteDto {
        public int IdServicio;
        [Required(ErrorMessage = "Definir fecha en transporte")]
        public string Fecha;
        public string Ciudad;
        [Required(ErrorMessage = "Definir hora de recojo en transporte")]
        public string HoraRecojo;
        [Required(ErrorMessage = "Definir hora de salida en transporte")]
        public string HoraSalida;
        public string Vuelo;
        [Required(ErrorMessage = "Definir nombre de transporte")]
        public string Servicio;
        [Required(ErrorMessage = "Definir cantidad de pasajeros en servicio")]
        public int Pasajeros;
        [Required(ErrorMessage = "Definir nombre de pasajero en servicio")]
        public string NombrePasajero;
        public string Vr;
        public string Tc;
        public string Transp;
        public string Obs;
        public string Hotel;
        public ServTransporteDto() {

        }
    }

    public class FileDto {
        [Required(ErrorMessage = "Codigo de file requerido")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "Biblia para file requerida")]
        public int IdBiblia { get; set; }
        //[Required(ErrorMessage = "Descripcion requerida")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Agencia para file requerida")]
        public int IdAgencia { get; set; }
        public string FechaCreacion { get; set; }
        [ValidateCollection]
        public List<ServServDto> Servicios { get; set; }
        [ValidateCollection]
        public List<ServTransporteDto> Transportes { get; set; }

        public FileDto() {
            Servicios = new List<ServServDto>();
            Transportes = new List<ServTransporteDto>();
        }

        public static bool Validar(FileDto dto, out List<string> outErrores) {
            List<string> errores = new List<string>();
            bool res = true;
            if (Utils.stringVacio(dto.Codigo)) {
                res = false;
                errores.Add("Codigo de file no definido");
            }
            if (dto.IdBiblia == 0) {
                res = false;
                errores.Add("Biblia de file no definida");
            }
            if (dto.IdBiblia == 0) {
                res = false;
                errores.Add("Agencia de file no definida");
            }
            int a = 0;
            foreach (var s in dto.Servicios) {
                a++;
                if (Utils.stringVacio(s.Fecha)) {
                    res = false;
                    errores.Add("Servicio " + a + " no tiene una fecha definida");
                }
                if (Utils.stringVacio(s.Servicio)) {
                    res = false;
                    errores.Add("Servicio " + a + " no tiene un nombre definido");
                }
                if (s.Pasajeros < 1) {
                    res = false;
                    errores.Add("Servicio " + a + " no tiene una cantidad de pasajeros mayor a 0");
                }
                if (Utils.stringVacio(s.NombrePasajero)) {
                    res = false;
                    errores.Add("Servicio " + a + " no tiene un nombre de pasajero principal");
                }
            }


            a = 0;
            foreach (var s in dto.Transportes) {
                a++;
                if (Utils.stringVacio(s.Fecha)) {
                    res = false;
                    errores.Add("Transporte " + a + " no tiene una fecha definida");
                }
                if (Utils.stringVacio(s.HoraRecojo)) {
                    res = false;
                    errores.Add("Transporte " + a + " no tiene una hora de recojo definida");
                }
                if (Utils.stringVacio(s.HoraSalida)) {
                    res = false;
                    errores.Add("Transporte " + a + " no tiene una hora de salida definida");
                }
                if (Utils.stringVacio(s.Servicio)) {
                    res = false;
                    errores.Add("Transporte " + a + " no tiene un nombre definido");
                }
                if (s.Pasajeros < 1) {
                    res = false;
                    errores.Add("Transporte " + a + " no tiene una cantidad de pasajeros mayor a 0");
                }
                if (Utils.stringVacio(s.NombrePasajero)) {
                    res = false;
                    errores.Add("Transporte " + a + " no tiene un nombre de pasajero principal");
                }
            }
            outErrores = errores;
            return res;
        }

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
                if (s.TipoServicio == Constantes.TipoServicio.Transporte) {
                    var tr = new ServTransporteDto() {
                        IdServicio = s.IdServicio,
                        Fecha = Utils.datetimeAString(s.Fecha),
                        Ciudad = s.Ciudad,
                        Servicio = s.Nombre,

                        Vuelo = s.Vuelo,
                        Pasajeros = s.Pasajeros,
                        NombrePasajero = s.NombrePasajero,
                        Vr = s.Vr,
                        Transp = s.Transporte,
                        Obs = s.Observaciones,
                        Tc = s.Tc
                    };

                    if (s.HoraRecojo.HasValue)
                        tr.HoraRecojo = Utils.timespanAStringHora(s.HoraRecojo.Value);
                    else
                        tr.HoraRecojo = "Hora no definida";

                    if (s.HoraSalida.HasValue)
                        tr.HoraSalida = Utils.timespanAStringHora(s.HoraSalida.Value);
                    else
                        tr.HoraSalida = "Hora no definida";

                    dto.Transportes.Add(tr);
                }
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
                    TipoServicio = Constantes.TipoServicio.Transporte,
                    Ciudad = trans.Ciudad,
                    Fecha = Utils.stringFechaADatetime(trans.Fecha),
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