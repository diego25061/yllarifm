﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YllariFM.Models.DB;

namespace CoreApp4.Controllers
{
    public class ServiciosController : Controller
    {
        private readonly YllariFMContext _context;

        public ServiciosController(YllariFMContext context)
        {
            _context = context;
        }

        // GET: Servicios
        public async Task<IActionResult> Index()
        {
            var yllariFMContext = _context.Servicio.Include(s => s.IdFileNavigation).Include(s => s.IdProveedorNavigation);
            return View(await yllariFMContext.ToListAsync());
        }

        // GET: Servicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicio
                .Include(s => s.IdFileNavigation)
                .Include(s => s.IdProveedorNavigation)
                .SingleOrDefaultAsync(m => m.IdServicio == id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // GET: Servicios/Create
        public IActionResult Create()
        {
            ViewData["IdFile"] = new SelectList(_context.File, "IdFile", "Codigo");
            ViewData["IdProveedor"] = new SelectList(_context.Proveedor, "IdProveedor", "Nombre");
            return View();
        }

        // POST: Servicios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdServicio,Fecha,TipoServicio,Nombre,IdFile,EsProvincia,HoraRecojo,HoraSalida,Vuelo,Pasajeros,Vr,Tc,IdProveedor,Observaciones")] Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(servicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdFile"] = new SelectList(_context.File, "IdFile", "Codigo", servicio.IdFile);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedor, "IdProveedor", "Nombre", servicio.IdProveedor);
            return View(servicio);
        }

        // GET: Servicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicio.SingleOrDefaultAsync(m => m.IdServicio == id);
            if (servicio == null)
            {
                return NotFound();
            }
            ViewData["IdFile"] = new SelectList(_context.File, "IdFile", "Codigo", servicio.IdFile);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedor, "IdProveedor", "Nombre", servicio.IdProveedor);
            return View(servicio);
        }

        // POST: Servicios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdServicio,Fecha,TipoServicio,Nombre,IdFile,EsProvincia,HoraRecojo,HoraSalida,Vuelo,Pasajeros,Vr,Tc,IdProveedor,Observaciones")] Servicio servicio)
        {
            if (id != servicio.IdServicio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicioExists(servicio.IdServicio))
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
            ViewData["IdFile"] = new SelectList(_context.File, "IdFile", "Codigo", servicio.IdFile);
            ViewData["IdProveedor"] = new SelectList(_context.Proveedor, "IdProveedor", "Nombre", servicio.IdProveedor);
            return View(servicio);
        }

        // GET: Servicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicio = await _context.Servicio
                .Include(s => s.IdFileNavigation)
                .Include(s => s.IdProveedorNavigation)
                .SingleOrDefaultAsync(m => m.IdServicio == id);
            if (servicio == null)
            {
                return NotFound();
            }

            return View(servicio);
        }

        // POST: Servicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servicio = await _context.Servicio.SingleOrDefaultAsync(m => m.IdServicio == id);
            _context.Servicio.Remove(servicio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicioExists(int id)
        {
            return _context.Servicio.Any(e => e.IdServicio == id);
        }
    }
}