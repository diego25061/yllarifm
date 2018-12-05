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
using YllariFM.Source.ViewModels.Vistas.Files;

namespace YllariFM.Controllers
{
    public class FilesController : BaseController
    {
        private readonly YllariFmContext _context;

        public FilesController(YllariFmContext context)
        {
            _context = context;
        }

        // GET: Files
        public async Task<IActionResult> Index()
        {
            /*
            var YllariFmContext = _context.File.Include(f => f.IdAgenciaNavigation).Include(f => f.IdBibliaNavigation);
            return View(await YllariFmContext.ToListAsync());
            */
            var files = _context.File.Include(f => f.IdAgenciaNavigation).Include(f => f.IdBibliaNavigation).Include(x=>x.Servicio);
            List<ListarFilesVm.File> lista = new List<ListarFilesVm.File>();
            foreach(var fi in files)
            {
                ListarFilesVm.File f = new ListarFilesVm.File()
                {
                    Agencia = fi.IdAgenciaNavigation.Nombre,
                    Codigo = fi.Codigo,
                    Descripcion = fi.Descripcion,
                    Estado = "Abierto",
                    FechaCreacion = Utils.formatoFecha(fi.FechaCreacion),
                    Id = fi.IdFile,
                    NombreBiblia = Utils.getNombreBiblia(fi.IdBibliaNavigation),
                    ServiciosServ = fi.Servicio.Where(x=>x.TipoServicio==Constantes.TipoServicio.Servicio && x.IdFile == fi.IdFile).Count(),
                    ServiciosTransporte = fi.Servicio.Where(x => x.TipoServicio == Constantes.TipoServicio.Transporte && x.IdFile == fi.IdFile).Count()
                };
                lista.Add(f);
            }

            return View(new ListarFilesVm() { Files = lista });
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

        // GET: Files/Actualizar/5
        public async Task<IActionResult> Actualizar(int? id)
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
            /*
            ViewData["IdAgencia"] = new SelectList(_context.Agencia, "IdAgencia", "CorreoContacto", file.IdAgencia);
            ViewData["IdBiblia"] = new SelectList(_context.Biblia, "IdBiblia", "IdBiblia", file.IdBiblia);
            return View(file);
            */
            return View(id.Value);
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


        [Route("api/Files/Crear")]
        [HttpPost]
        public ActionResult Crear([FromBody] CreateFileVm vm)
        {
            Trace.WriteLine("Creando " + Json(vm).Value);
            try
            {
                if (ModelState.IsValid)
                {
                    //YllariFmContext context = new YllariFmContext();
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
            var YllariFmContext = _context.File.Include(f => f.IdAgenciaNavigation).Include(f => f.IdBibliaNavigation);
            return View("Index", YllariFmContext.ToListAsync());
        }*/

        [Route("api/Files/{id}")]
        public JsonResult Detalles(int id)
        {
            var file = _context.File.Include(x=>x.Servicio).Where(x => x.IdFile == id).FirstOrDefault();
            if (file == null)
            {
                return Json(CrearContRespuestaTransaccion("", "File no encontrado"), StatusCodes.Status404NotFound);
            }

            var fileResultante = CreateFileVm.fromDb(file);
            return Json(fileResultante);

            
        }






















        /*
        [Route("api/Files/actualizar/{id}")]
        public ActionResult Editar(int id)
        {
            return View();
        }*/



        //TODO ARREGLAR ESTA WA
        //[Route("api/Files/actualizar/{id}")]
        public ActionResult Editar(int id, [FromBody]CreateFileVm vm)
        {
            //var context = new YllariFmContext();
            if (ModelState.IsValid)
            {
                try
                {
                    var actual = _context.File.Where(x => x.IdFile == id).FirstOrDefault();
                    var editado = vm.toDbFile();
                    //editando campos
                    actual.Descripcion = editado.Descripcion;
                    actual.IdAgencia = editado.IdAgencia;
                    actual.IdBiblia = editado.IdBiblia;
                    actual.Servicio = editado.Servicio;
                    //-------
                    _context.SaveChanges();
                    Debug.WriteLine("Objeto grabado");
                    return Json("success");
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("Error");
                    return Json("error " + ex.Message);
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
