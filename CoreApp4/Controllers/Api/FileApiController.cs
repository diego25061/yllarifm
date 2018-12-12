using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YllariFM.Models.DB;
using YllariFM.Source;
using YllariFM.Source.ViewModels;
using YllariFM.Source.ViewModels.Api;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YllariFM.Controllers.Api {
    [Route("api/files")]
    public class FileApiController : BaseController {
        private readonly YllariFmContext _context;
        public FileApiController(YllariFmContext context) {
            _context = context;
        }

        [HttpGet("todo")]
        public ActionResult Get() {
            try {
                var files = _context.File.Include(f => f.IdClienteNavigation).Include(f => f.IdBibliaNavigation).Include(x => x.Servicio).ToList();
                var lista = FileDto.GenerarDtos(files);
                return Json(lista);
            } catch (Exception ex) {
                return Json(new Respuesta("", ex), StatusCodes.Status500InternalServerError);
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id) {
            try {
                var file = _context.File.Include(f => f.IdClienteNavigation).Include(f => f.IdBibliaNavigation).Include(x => x.Servicio).Where(x => x.IdFile == id).FirstOrDefault();
                if (file == null) {
                    return Json(new Respuesta("File " + id + " no encontrado"), StatusCodes.Status503ServiceUnavailable);
                } else {
                    return Json(FileDto.GenerarDto(file));
                }
            } catch (Exception ex) {
                return Json(new Respuesta("", ex), StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<controller>
        [HttpPost("guardar")]
        public ActionResult Post([FromBody] FileDto dto ) {
            try {
                
                if (dto == null)
                    return Json(new Respuesta("File a guardar nulo"), StatusCodes.Status400BadRequest);
                var existente = _context.File.Where(x => x.Codigo == dto.Codigo).FirstOrDefault();
                if(existente!=null)
                    return Json(new Respuesta("Un file con codigo '"+dto.Codigo+"' ya existe. Elegir otro código."), StatusCodes.Status400BadRequest);

                List<string> errores = new List<string>();
                if(!FileDto.Validar(dto,out errores)) {
                    return Json(new Respuesta("Error al crear file: </br>" + Utils.listaStringsAListaHtml(errores)), StatusCodes.Status400BadRequest);
                }
                /*
                if (!ModelState.IsValid) {
                    string errores = Utils.getHtmlStringErroresModelState(ModelState);
                    return Json(new Respuesta("Error al guardar file: </br>" + errores), StatusCodes.Status400BadRequest);
                }*/
                var dbo = FileDto.GenerarDbo(dto);
                _context.File.Add(dbo);
                _context.SaveChanges();
                return Json(dbo.IdFile);
            } catch (Exception ex) {
                return Json(new Respuesta("", ex), StatusCodes.Status500InternalServerError);
            }
        }

        //TODO BUGEADO, PROBAR BIEN!
        [HttpPost("actualizar/{id}")]
        public ActionResult PutFileAndServices(int id, [FromBody] FileDto dto) {

            using (var transaction = _context.Database.BeginTransaction()) {
                try {
                    var encontrado = _context.File.Include(f => f.IdClienteNavigation).Include(f => f.IdBibliaNavigation).Include(x => x.Servicio).Where(x => x.IdFile == id).FirstOrDefault();
                    if (encontrado == null) {
                        transaction.Rollback();
                        return Json(new Respuesta("El file a actualizar no existe"), StatusCodes.Status500InternalServerError);
                    } else {
                        var obj = FileDto.GenerarDbo(dto);


                        encontrado.Codigo = obj.Codigo;
                        encontrado.IdBiblia = obj.IdBiblia;
                        encontrado.Descripcion = obj.Descripcion;
                        encontrado.IdCliente= obj.IdCliente;
                        //encontrado.Servicio = obj.Servicio;
                        List<int> sobrantes = new List<int>();
                        foreach (var s in encontrado.Servicio) {
                            sobrantes.Add(s.IdServicio);
                        }

                        foreach (var s in obj.Servicio) {
                            var servEncontrado = _context.Servicio.Where(x => x.IdServicio == s.IdServicio).FirstOrDefault();
                            if (servEncontrado == null) {
                                s.IdFile = id;
                                _context.Servicio.Add(s);
                            } else {
                                sobrantes.Remove(servEncontrado.IdServicio);

                                servEncontrado.Nombre = s.Nombre;
                                servEncontrado.HoraRecojo = s.HoraRecojo;
                                servEncontrado.HoraSalida = s.HoraSalida;
                                servEncontrado.Vuelo = s.Vuelo;
                                servEncontrado.Pasajeros = s.Pasajeros;
                                servEncontrado.Vr = s.Vr;
                                servEncontrado.Tc = s.Tc;
                                servEncontrado.Hotel = s.Hotel;
                                servEncontrado.IdProveedor = s.IdProveedor;
                                servEncontrado.Observaciones = s.Observaciones;
                                servEncontrado.Ciudad = s.Ciudad;
                                servEncontrado.NombrePasajero = s.NombrePasajero;
                                servEncontrado.Tren = s.Tren;
                                servEncontrado.Alm = s.Alm;
                                servEncontrado.Transporte = s.Transporte;
                                servEncontrado.Hotel = s.Hotel;
                                _context.Servicio.Update(servEncontrado);
                            }
                        }
                        //borrando sobrantes
                        foreach (var a in sobrantes) {
                            var ssobrante = _context.Servicio.Where(x => x.IdServicio == a).FirstOrDefault();
                            _context.Servicio.Remove(ssobrante);
                        }
                        _context.SaveChanges();

                        var devolucion = FileDto.GenerarDto(encontrado);
                        transaction.Commit();
                        return Json(devolucion);
                    }
                } catch (Exception ex) {
                    transaction.Rollback();
                    return Json(new Respuesta("", ex), StatusCodes.Status500InternalServerError);
                }
            }
        }

        // DELETE api/<controller>/5
        [HttpPost("borrar/{id}")]
        public ActionResult Delete(int id) {
            using (var transaction = _context.Database.BeginTransaction()) {
                try {
                    var encontrado = _context.File.Where(x => x.IdFile == id).FirstOrDefault();
                    if (encontrado == null)
                        return Json(new Respuesta("El file " + id + " no existe"), StatusCodes.Status500InternalServerError);
                    else {
                        int a = encontrado.IdFile;

                        foreach (var f in encontrado.Servicio) {
                            _context.Servicio.Remove(f);
                        }
                        _context.File.Remove(encontrado);
                        transaction.Commit();
                        return Json(a);
                    }
                } catch (Exception ex) {
                    transaction.Rollback();
                    return Json(new Respuesta("", ex), StatusCodes.Status500InternalServerError);
                }
            }
        }
    }
}
