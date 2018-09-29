using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YllariFM.Source.ViewModels;
using System.Diagnostics;
using YllariFM.Models.DB;
using YllariFM.Source;
using Microsoft.AspNetCore.Http;

namespace YllariFM.Controllers
{
    public class FilesController : BaseController
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

        //===================================================================== api


        [Route("Files/Crear")]
        [HttpPost]
        public ActionResult Crear([FromBody] CreateFileVm vm)
        {
            Trace.WriteLine("Creando " + Json(vm).Value);
            try
            {
                if (ModelState.IsValid)
                {
                    //YllariFMContext context = new YllariFMContext();
                    File file = vm.toDbFile();
                    _context.File.Add(file);
                    _context.SaveChanges();
                    return Json(CrearContRespuestaTransaccion("File guardado exitosamente",""));
                }
                else
                {
                    return Json(CrearContRespuestaTransaccion("","Datos incorrectos"), StatusCodes.Status400BadRequest);
                }

                
            }catch(Exception ex)
            {
                //return Json(Utils.GetFullMensajeExcepcion(ex) + "<br>" + ex.StackTrace);
                string msjError = Utils.GetFullMensajeExcepcion(ex) + "<br>" + ex.StackTrace;
                return Json(CrearContRespuestaTransaccion("", msjError), StatusCodes.Status500InternalServerError);
            }
            
        }

        [Route("Files/Crear")]
        public ActionResult Crear()
        {
            return View("Crear");
        }
        /*
        [Route("Files")]
        public ActionResult Listar()
        {
            var yllariFMContext = _context.File.Include(f => f.IdAgenciaNavigation).Include(f => f.IdBibliaNavigation);
            return View("Index", yllariFMContext.ToListAsync());
        }*/

        [Route("Files/{id}")]
        public ActionResult Detalles(int id)
        {
            return View();
        }

        [Route("Files/Editar/{id}")]
        public ActionResult Editar(int id)
        {
            return View();
        }

        [Route("Files/Editar/{id}")]
        public ActionResult Editar(int id, CreateFileVm vm)
        {
            var context = new YllariFMContext();
            if (ModelState.IsValid)
            {
                try
                {
                    var actual = context.File.Where(x => x.IdFile == id).FirstOrDefault();
                    actual = vm.toDbFile();
                    context.SaveChanges();
                    Debug.WriteLine("Objeto grabado");
                    return Json("success");
                }
                catch
                {
                    Debug.WriteLine("Error");
                    return Json("error");
                }
            }
            else
            {
                return Json("error2");
            }
        }


            /*
        [Route("api/file/{id}")]
        public JsonResult getFile(int id)
        {
            File file = _context.File.Where(x => x.IdFile == id).FirstOrDefault();
            if (file != null) {

                List<object> servs = new List<object>();
                List<object> transps = new List<object>();

                foreach (var v in file.Servicio)
                {
                    servs.Add(new {
                        ciudad = v.id
                    });
                }

                object respuesta = new {
                    codigo = file.Codigo,
                    biblia = file.IdBiblia,
                    servicios = file.Servicio
                }
            }
            else
            {
                return Json("No encontrado");
            }
        }
        
         */

    }
}
