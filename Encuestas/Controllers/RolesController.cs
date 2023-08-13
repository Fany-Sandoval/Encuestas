using Encuestas.Models;
using Estadisticas.Negocios;
using Estadisticas.Negocios.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Estadisticas.Modelo;

namespace Encuestas.Controllers
{
    public class RolesController : Controller
    {
        IRolesNegocio context = new RolesNegocio();
        // GET: Roles
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind (Include ="strValor")] RolesViewModel rol)
        {
            if (ModelState.IsValid)
            {
                var Roles = new Roles { strValor = rol.strValor };
                context.Insert(Roles);
            }
            return View();
        }
    }
}