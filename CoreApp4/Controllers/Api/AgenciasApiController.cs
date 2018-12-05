using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YllariFM.Models.DB;
using YllariFM.Source.ViewModels;
using YllariFM.Source.ViewModels.Api;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YllariFM.Controllers.Api {
    [Route("api/agencias/")]
    public class AgenciasApiController : BaseController {
        private readonly YllariFmContext _context;
        public AgenciasApiController(YllariFmContext context) {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet("todo")]
        public ActionResult Get() {
            try {
                var agencias = _context.Agencia.ToList();
                var lista = Mapper.Map<List<Agencia>, List<AgenciaDto>>(agencias);
                return Json(lista);
            } catch (Exception ex) {
                return Json(new Respuesta("", ex), StatusCodes.Status500InternalServerError);
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id) {
            try {
                var agencia = _context.Agencia.Where(x => x.IdAgencia == id).FirstOrDefault();
                if (agencia != null) {
                    var ag = Mapper.Map<Agencia, AgenciaDto>(agencia);
                    return Json(ag);
                } else
                    return Json(new Respuesta("Agencia " + id + " no encontrada"), StatusCodes.Status500InternalServerError);
            } catch (Exception ex) {
                return Json(new Respuesta("", ex), StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<controller>
        [HttpPost("guardar")]
        public ActionResult Post([FromBody] AgenciaDto agencia) {
            try {
                var dbo = Mapper.Map<AgenciaDto, Agencia>(agencia);
                _context.Agencia.Add(dbo);
                _context.SaveChanges();
                return Json(dbo.IdAgencia);
            } catch (Exception ex) {
                return Json(new Respuesta("", ex), StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<controller>/5
        [HttpPost("actualizar/{id}")]
        public ActionResult Put(int id, [FromBody]ActualizarAgenciaDto agencia) {
            try {
                var encontrada = _context.Agencia.Where(x => x.IdAgencia == id).FirstOrDefault();
                if (encontrada == null)
                    return Json(new Respuesta("La agencia a actualizar no existe"), StatusCodes.Status500InternalServerError);
                else {
                    var obj = Mapper.Map<ActualizarAgenciaDto, Agencia>(agencia, encontrada);
                    _context.SaveChanges();
                    var devolucion = Mapper.Map<Agencia, ActualizarAgenciaDto>(obj);
                    return Json(devolucion);
                }
            } catch (Exception ex) {
                return Json(new Respuesta("", ex), StatusCodes.Status500InternalServerError);
            }

        }

        // DELETE api/<controller>/5
        [HttpPost("eliminar/{id}")]
        public ActionResult Delete(int id) {
            try {
                var encontrada = _context.Agencia.Where(x => x.IdAgencia == id).FirstOrDefault();
                if (encontrada == null)
                    return Json(new Respuesta("La agencia a eliminar no existe"), StatusCodes.Status500InternalServerError);
                else {
                    _context.Agencia.Remove(encontrada);
                    int a = encontrada.IdAgencia;
                    _context.SaveChanges();
                    return Json(a);
                }
            } catch (Exception ex) {
                return Json(new Respuesta("", ex), StatusCodes.Status500InternalServerError);
            }
        }
    }
}
