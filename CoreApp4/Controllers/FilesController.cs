using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreApp4.Models.DB;

namespace CoreApp4.Controllers
{
    public class FilesController : Controller
    {
        private readonly YllariFMContext _context;

        public FilesController(YllariFMContext context)
        {
            _context = context;
        }

        // GET: Files
        public async Task<IActionResult> Index()
        {
            var yllariFMContext = _context.File.Include(f => f.IdAgenciaNavigation).Include(f => f.IdBibliaNavigation);
            return View(await yllariFMContext.ToListAsync());
        }

        // GET: Files/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _context.File
                .Include(f => f.IdAgenciaNavigation)
                .Include(f => f.IdBibliaNavigation)
                .SingleOrDefaultAsync(m => m.IdFile == id);
            if (file == null)
            {
                return NotFound();
            }

            return View(file);
        }

        // GET: Files/Create
        public IActionResult Create()
        {
            ViewData["IdAgencia"] = new SelectList(_context.Agencia, "IdAgencia", "CorreoContacto");
            ViewData["IdBiblia"] = new SelectList(_context.Biblia, "IdBiblia", "IdBiblia");
            return View();
        }

        // POST: Files/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,IdFile,IdBiblia,Descripcion,IdAgencia")] File file)
        {
            if (ModelState.IsValid)
            {
                _context.Add(file);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAgencia"] = new SelectList(_context.Agencia, "IdAgencia", "CorreoContacto", file.IdAgencia);
            ViewData["IdBiblia"] = new SelectList(_context.Biblia, "IdBiblia", "IdBiblia", file.IdBiblia);
            return View(file);
        }

        // GET: Files/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _context.File.SingleOrDefaultAsync(m => m.IdFile == id);
            if (file == null)
            {
                return NotFound();
            }
            ViewData["IdAgencia"] = new SelectList(_context.Agencia, "IdAgencia", "CorreoContacto", file.IdAgencia);
            ViewData["IdBiblia"] = new SelectList(_context.Biblia, "IdBiblia", "IdBiblia", file.IdBiblia);
            return View(file);
        }

        // POST: Files/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,IdFile,IdBiblia,Descripcion,IdAgencia")] File file)
        {
            if (id != file.IdFile)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(file);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileExists(file.IdFile))
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
            ViewData["IdAgencia"] = new SelectList(_context.Agencia, "IdAgencia", "CorreoContacto", file.IdAgencia);
            ViewData["IdBiblia"] = new SelectList(_context.Biblia, "IdBiblia", "IdBiblia", file.IdBiblia);
            return View(file);
        }

        // GET: Files/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var file = await _context.File
                .Include(f => f.IdAgenciaNavigation)
                .Include(f => f.IdBibliaNavigation)
                .SingleOrDefaultAsync(m => m.IdFile == id);
            if (file == null)
            {
                return NotFound();
            }

            return View(file);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var file = await _context.File.SingleOrDefaultAsync(m => m.IdFile == id);
            _context.File.Remove(file);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileExists(int id)
        {
            return _context.File.Any(e => e.IdFile == id);
        }
    }
}
