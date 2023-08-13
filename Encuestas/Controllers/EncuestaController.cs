using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Encuestas.Controllers
{
    public class EncuestaController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "Administrador, Ventas")]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Mensaje = "Esta es la consulta de encuestas";
                return View();
            }
            return RedirectToAction("AccesoDenegado", "Error");
        }
    }
}