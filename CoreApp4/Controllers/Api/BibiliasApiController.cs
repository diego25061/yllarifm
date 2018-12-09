using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YllariFM.Models.DB;
using YllariFM.Source;
using YllariFM.Source.ViewModels;
using static YllariFM.Source.ViewModels.BibliasVms;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YllariFM.Controllers.Api {
    [Route("api/biblias")]
    public class BibiliasApiController : BaseController {
        private readonly YllariFmContext _context;
        public BibiliasApiController(YllariFmContext context) {
            _context = context;
        }
        // GET: api/<controller>
        [HttpGet("todo")]
        public ActionResult Get() {
            try {
                var biblias = _context.Biblia.ToList();
                var lista = Mapper.Map<List<Biblia>, List<BibliaDto>>(biblias);
                return Json(lista);
            } catch (Exception ex) {
                return Json(new Respuesta("", ex), StatusCodes.Status500InternalServerError);
            }
        }

        //TODO necesario xq no puedo modificar desde la view :c .Aprender react!
        [HttpGet("todoNombre")]
        public ActionResult GetNombre() {
            try {
                var biblias = _context.Biblia.ToList();
                List<object> lista = new List<object>();
                foreach(var b in biblias) {
                    lista.Add(new { Nombre = Utils.MesIntATexto(b.Mes)+" "+b.Anho , IdBiblia = b.IdBiblia});
                }
                return Json(lista);
            } catch (Exception ex) {
                return Json(new Respuesta("", ex), StatusCodes.Status500InternalServerError);
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id) {
            return "value";
        }

        // POST api/<controller>
        [HttpPost("guardar")]
        public ActionResult Post([FromBody] BibliaDto biblia) {
            try {
                if(biblia.Mes>12 || biblia.Mes < 1)
                    return Json(new Respuesta("Mes invalido: "+biblia.Mes), StatusCodes.Status500InternalServerError);
                var dbo = Mapper.Map<BibliaDto, Biblia>(biblia);
                _context.Biblia.Add(dbo);
                _context.SaveChanges();
                return Json(dbo.IdBiblia);
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
