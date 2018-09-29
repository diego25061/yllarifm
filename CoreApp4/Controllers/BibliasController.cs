using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YllariFM.Models.DB;

namespace CoreApp4.Controllers
{
    public class BibliasController : Controller
    {
        private readonly YllariFMContext _context;

        public BibliasController(YllariFMContext context)
        {
            _context = context;
        }

        // GET: Biblias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Biblia.ToListAsync());
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


        //===================================================================== api

        [Route("api/biblias")]
        public JsonResult listaBiblias()
        {
            var lista = _context.Biblia.ToList();
            List<object> respuesta = new List<object>();
            foreach(var b in lista)
            {
                respuesta.Add(new { id = b.IdBiblia , nombre = b.Mes + b.Anho });
            }
            return Json(respuesta);
        }
    }
}
