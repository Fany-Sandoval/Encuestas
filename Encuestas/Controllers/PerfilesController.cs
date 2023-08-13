using Encuestas.Models;
using Estadisticas.Modelo;
using Estadisticas.Negocios;
using Estadisticas.Negocios.Contrato;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Encuestas.Controllers
{
    public class PerfilesController : Controller
    {
        private readonly IPerfilesNegocio db = new PerfilesNegocio();
        private readonly IUsuarioNegocio udb = new UsuariosNegocio();
        // GET: Perfiles
        [HttpGet]
        public ActionResult Registrar()
        {
            ViewBag.IdUser = new SelectList(udb.GetUsuarios(), "Id", "email");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar([Bind(Include = "nombre, apellidoPaterno, apellidoMaterno, edad, fechaNacimiento", Exclude ="Foto")] PerfilesViewModel per, int IdUser)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase poImgFile = Request.Files["Foto"];

                    using (var binary = new BinaryReader(poImgFile.InputStream))
                    {
                        imageData = binary.ReadBytes(poImgFile.ContentLength);
                    }
                }

                Perfiles perfil = new Perfiles { Nombre = per.Nombre, ApellidoPaterno = per.ApellidoPaterno, ApellidoMaterno = per.ApellidoMaterno, Edad = per.Edad, FechaNacimiento = per.FechaNacimiento }; 
                List<Usuarios> usuarios = new List<Usuarios>();
                Usuarios usuario = new Usuarios { Id = IdUser };
                perfil.IdUser = IdUser;
                perfil.Foto = imageData;
                if (db.Insert(perfil))
                {
                    return RedirectToAction("SigIn", "Seguridad");
                }
                else
                {
                    ModelState.AddModelError("error_registro", "El perfil no se registro correctamente");
                    return View();
                }
            }
            ViewBag.IdUser = new SelectList(udb.GetUsuarios(), "Id", "email");
            return View();
        }
    }
}