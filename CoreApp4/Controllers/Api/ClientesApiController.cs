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
using YllariFM.Source.ViewModels.Api;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YllariFM.Controllers.Api {
    [Route("api/clientes")]
    public class ClientesApiController : BaseController {

        private readonly YllariFmContext _context;
        public ClientesApiController(YllariFmContext context) {
            _context = context;
        }

        // GET: api/<controller>
        [HttpGet("todo")]
        public ActionResult Get() {
            
            try {
                var clientes = _context.Cliente.ToList();
                var lista = new List<ClienteDto>();
                foreach(var c in clientes) {
                    lista.Add(ClienteDto.generarDto(c));
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
        public ActionResult Post([FromBody] ClienteDto cliente) {
            try {
                if (Utils.stringLleno(cliente.CorreoContacto))
                    if (!Utils.IsValidEmail(cliente.CorreoContacto))
                        return Json(new Respuesta("Correo de contacto invalido"), StatusCodes.Status400BadRequest);


                var dbo = ClienteDto.generarDbo(cliente);
                var existente = _context.Cliente.Where(x => x.Nombre == dbo.Nombre).FirstOrDefault();
                if (existente != null)
                    return Json(new Respuesta("Ya existe un cliente con nombre '"+cliente.Nombre+"'."), StatusCodes.Status400BadRequest);

                dbo.IdCliente = 0;
                //var dbo = Mapper.Map<AgenciaDto, Agencia>(agencia);
                _context.Cliente.Add(dbo);
                _context.SaveChanges();
                return Json(dbo.IdCliente);
            } catch (Exception ex) {
                return Json(new Respuesta("Error al guardar cliente", ex), StatusCodes.Status500InternalServerError);
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
