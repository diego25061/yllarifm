using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YllariFM.Controllers;
using YllariFM.Models.DB;
using YllariFM.Source;
using YllariFM.Source.ViewModels.Vistas.Files;

namespace CoreApp4.Controllers
{
    public class BibliasController : BaseController
    {
        private readonly YllariFmContext _context;

        public BibliasController(YllariFmContext context)
        {
            _context = context;
        }

        // GET: Biblias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Biblia.OrderByDescending(x=> x.Anho ).ToListAsync());
        }

        // GET: Biblias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var biblia = await _context.Biblia
                .SingleOrDefaultAsync(m => m.IdBiblia == id);
            if (biblia == null)
            {
                return NotFound();
            }

            return View(biblia);
        }

        // GET: Biblias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Biblias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBiblia,Mes,Anho")] Biblia biblia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(biblia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(biblia);
        }

        // GET: Biblias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var biblia = await _context.Biblia.SingleOrDefaultAsync(m => m.IdBiblia == id);
            if (biblia == null)
            {
                return NotFound();
            }
            return View(biblia);
        }

        // POST: Biblias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBiblia,Mes,Anho")] Biblia biblia)
        {
            if (id != biblia.IdBiblia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(biblia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BibliaExists(biblia.IdBiblia))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(biblia);
        }

        // GET: Biblias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var biblia = await _context.Biblia
                .SingleOrDefaultAsync(m => m.IdBiblia == id);
            if (biblia == null)
            {
                return NotFound();
            }

            return View(biblia);
        }

        // POST: Biblias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var biblia = await _context.Biblia.SingleOrDefaultAsync(m => m.IdBiblia == id);
            _context.Biblia.Remove(biblia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BibliaExists(int id)
        {
            return _context.Biblia.Any(e => e.IdBiblia == id);
        }

        [Route("biblias/{id}")]
        public ActionResult detalleBiblia(int id)
        {
            var biblia = _context.Biblia.Where(x => x.IdBiblia == id).FirstOrDefault();
            DateTime date = DateTime.Now;
            if (biblia != null)
                date = new DateTime(biblia.Anho, biblia.Mes, 1);

            DetalleBibliaVm vm = new DetalleBibliaVm();
            vm.Fecha = Utils.datetimeAString(date);
            return View("detalle", vm);
        }

        //===================================================================== api

        [Route("api/biblias")]
        public JsonResult listaBiblias()
        {
            var lista = _context.Biblia.ToList();
            lista.OrderByDescending(x => Convert.ToInt16(x.Anho.ToString("0000" + "" + x.Mes.ToString("00"))));
            List<object> respuesta = new List<object>();
            foreach (var b in lista)
            {
                respuesta.Add(new { id = b.IdBiblia, nombre = Utils.getNombreBiblia(b) });
            }
            return Json(respuesta);
        }

        /*
        [Route("api/biblias/actualFecha")]
        public JsonResult bibliaActual()
        {
            int mes = Utils.fechaPeruanaActual().Month;
            int anho = Utils.fechaPeruanaActual().Year;
            Trace.WriteLine("mes actual:" + mes);
            return Json(new
            {
                mes,anho,fechaPeruana=Utils.fechaPeruanaActual()
            });
        }*/


        [Route("api/Biblias/Servicios/Eventos")]
        public JsonResult eventosServiciosServs([FromQuery(Name = "start")] string fechaInicio, [FromQuery(Name = "end")]string fechaFin)
        {
            if (fechaInicio == null)
                return Json("Fecha inicial no especificada", StatusCodes.Status400BadRequest);
            if (fechaFin == null)
                return Json("Fecha final no especificada", StatusCodes.Status400BadRequest);

            DateTime desde = DateTime.Parse(fechaInicio);
            DateTime hasta = DateTime.Parse(fechaFin);
            var servicios = _context.Servicio.Where(x => x.Fecha > desde && x.Fecha < hasta && x.TipoServicio == Constantes.TipoServicio.Servicio);
            List<object> rpta = new List<object>();
            foreach (var s in servicios)
            {
                rpta.Add(new
                {
                    title = s.Nombre,
                    start = s.Fecha,
                    url = "api/servicios/" + s.IdServicio
                });
            }
            return Json(rpta);
        }

        [Route("api/Biblias/Transportes/Eventos")]
        public JsonResult eventosServiciosTransporte([FromQuery(Name = "start")] string fechaInicio, [FromQuery(Name = "end")]string fechaFin)
        {
            if (fechaInicio == null)
                return Json("Fecha inicial no especificada", StatusCodes.Status400BadRequest);
            if (fechaFin == null)
                return Json("Fecha final no especificada", StatusCodes.Status400BadRequest);

            DateTime desde = DateTime.Parse(fechaInicio);
            DateTime hasta = DateTime.Parse(fechaFin);
            var servicios = _context.Servicio.Where(x => x.Fecha > desde && x.Fecha < hasta && x.TipoServicio == Constantes.TipoServicio.Transporte);
            List<object> rpta = new List<object>();
            foreach (var s in servicios)
            {
                rpta.Add(new
                {
                    title = s.Nombre,
                    start = s.Fecha,
                    url = "api/transportes/" + s.IdServicio
                });
            }
            return Json(rpta);
        }
    }
}
