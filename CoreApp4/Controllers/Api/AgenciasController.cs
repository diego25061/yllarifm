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
    public class AgenciasController : BaseController {
        private readonly YllariFmContext _context;
        //MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Agencia, AgenciaDto>());
        //IMapper mapper;
        public AgenciasController(YllariFmContext context) {
            _context = context;
            //mapper = config.CreateMapper();
        }

        // GET: api/<controller>
        [HttpGet("todo")]
        public ActionResult Get() {
            var agencias = _context.Agencia.ToList();
            //var mapper = config.CreateMapper();
            var lista = Mapper.Map<List<Agencia>, List<AgenciaDto>>(agencias);
            return Json(lista);
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
                    return Json(new Respuesta("Agencia "+id+" no encontrada"), StatusCodes.Status404NotFound);
            } catch (Exception ex) {
                return Json(new Respuesta("", ex), StatusCodes.Status500InternalServerError);
            }
        }

        // POST api/<controller>
        [HttpPost("guardar")]
        public JsonResult Post([FromBody] GrabarAgenciaDto agencia) {
            try {
                var dbo = Mapper.Map<GrabarAgenciaDto, Agencia>(agencia);
                _context.Agencia.Add(dbo);
                _context.SaveChanges();
                return Json(dbo.IdAgencia);
            } catch (Exception ex) {
                return Json(new Respuesta("", ex), StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
