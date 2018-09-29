using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YllariFM.Source.ViewModels;

namespace YllariFM.Controllers
{
    public class BaseController : Controller
    {

        public override JsonResult Json(object data)
        {
            return Json(data, new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore });
        }
        public ContenedorRespuesta CrearContRespuestaTransaccion(string contenido,string error)
        {
            return new ContenedorRespuesta(contenido,error);
        }

        public JsonResult Json(object objeto, int statusCode)
        {
            return new JsonResult(objeto)
            {
                StatusCode = statusCode
            };
        }
    }
}