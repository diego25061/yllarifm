using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YllariFM.Models.DB;

namespace CoreApp4.Controllers
{
    public class AgenciasController : Controller
    {
        private readonly YllariFmContext _context;
        
        public AgenciasController(YllariFmContext context)
        {
            _context = context;
        }


        // GET: Agencias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Agencia.ToListAsync());
        }

        // GET: Agencias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agencia = await _context.Agencia
                .SingleOrDefaultAsync(m => m.IdAgencia == id);
            if (agencia == null)
            {
                return NotFound();
            }

            return View(agencia);
        }

        // GET: Agencias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAgencia,Nombre,CorreoContacto,CorreoExtra,Pais,Ciudad")] Agencia agencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agencia);
        }

        // GET: Agencias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agencia = await _context.Agencia.SingleOrDefaultAsync(m => m.IdAgencia == id);
            if (agencia == null)
            {
                return NotFound();
            }
            return View(agencia);
        }

        // POST: Agencias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAgencia,Nombre,CorreoContacto,CorreoExtra,Pais,Ciudad")] Agencia agencia)
        {
            if (id != agencia.IdAgencia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgenciaExists(agencia.IdAgencia))
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
            return View(agencia);
        }

        // GET: Agencias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agencia = await _context.Agencia
                .SingleOrDefaultAsync(m => m.IdAgencia == id);
            if (agencia == null)
            {
                return NotFound();
            }

            return View(agencia);
        }

        // POST: Agencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agencia = await _context.Agencia.SingleOrDefaultAsync(m => m.IdAgencia == id);
            _context.Agencia.Remove(agencia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgenciaExists(int id)
        {
            return _context.Agencia.Any(e => e.IdAgencia == id);
        }


        [Route("api/Agencias")]
        public JsonResult ListaSelect()
        {
            //object a = new { a = "a" };
            List<object> objs = new List<object>();
            var agencias = _context.Agencia.ToList();
            foreach(var a in agencias){
                objs.Add(new { id = a.IdAgencia, nombre = a.Nombre });
            }
            return Json(objs);
        }

    }
}
