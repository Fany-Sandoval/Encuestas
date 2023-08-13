using Estadisticas.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Estadisticas.Negocios;
using Estadisticas.Negocios.Contrato;
using Encuestas.Models;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin;

namespace Encuestas.Controllers
{
    public class SeguridadController : Controller
    {
        private readonly IUsuarioNegocio db = new UsuariosNegocio();
        private readonly IRolesNegocio rdb = new RolesNegocio();
        // GET: Seguridad
        [HttpGet]
        public ActionResult SigIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SigIn([Bind(Include = "email,contrasenia")] UsuarioViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuarios usuario = new Usuarios { email = user.email, contrasenia = user.contrasenia };
                    Usuarios objUser = db.Validar(usuario);
                    var rol = new Roles();
                    if (objUser != null)
                    {
                        foreach (var r in objUser.UsuarioRol)
                        {
                            rol.Id = r.Roles.Id;
                            rol.strValor = r.Roles.strValor;
                        }
                        user.Id = objUser.Id;
                        user.RolesViewModel = new RolesViewModel { Id = rol.Id, strValor = rol.strValor };
                        this.SigInUser(user, true, null);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("error_login", "Usuario no encontrado");
                        return View();
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.IdRol = new SelectList(rdb.GetRoles(), "Id", "strValor");
            return View();
        }

        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "email, contrasenia")] UsuarioViewModel user, int IdRol)
        {
            if (ModelState.IsValid)
                using (Aes myAes = Aes.Create())
                {
                    // string user.contrasenia = "Here is some data to encrypt!";
                    byte[] encrypted = EncryptStringToBytes_Aes(user.contrasenia, myAes.Key, myAes.IV);
                    string cadena = Encoding.UTF8.GetString(encrypted);


                    Usuarios usuario = new Usuarios { email = user.email, contrasenia = cadena }; // Encriptación AES, Sha1, Sha5
                    List<UsuarioRol> usuariosRoles = new List<UsuarioRol>();
                    UsuarioRol usuarioRol = new UsuarioRol { IdRol = IdRol, Usuarios = usuario };
                    usuariosRoles.Add(usuarioRol);
                    usuario.UsuarioRol = usuariosRoles;
                    if (db.Insert(usuario))
                    {
                        return RedirectToAction("SigIn", "Seguridad");
                    }
                    else
                    {
                        ModelState.AddModelError("error_registro", "El usuario no se registro correctamente");
                        return View();
                    }
                }
            ViewBag.IdRol = new SelectList(rdb.GetRoles(), "Id", "strValor");
            return View();
        }

        [HttpGet]
        public ActionResult Usuarios()
        {
            try
            {
                List<UsuarioViewModel> usuarios = db.GetUsuarios().Select(
                    p => new UsuarioViewModel {
                        Id = p.Id, email = p.email, contrasenia = p.contrasenia }).ToList();
                
                return View(usuarios);
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
            }
            return View();
        }
        //[HttpDelete]
        //public ActionResult Eliminar(int id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            if (db.Delete(id))
        //            {
        //                return RedirectToAction("Index", "Home");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("error_borrado", "El usuario no se pudo borrar");
        //                return View();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            string mensaje = ex.Message;
        //        }
        //    }
        //    return View();
        //}

        private ActionResult SigInUser(UsuarioViewModel userVM, bool rememberMe, string returnUrl)
        {
            ActionResult Result;
            List<Claim> Claims = new List<Claim>(); //Listado de claims
            Claims.Add(new Claim(ClaimTypes.NameIdentifier, userVM.Id.ToString()));
            Claims.Add(new Claim(ClaimTypes.Email, userVM.email));
            if (userVM.RolesViewModel != null)
            {
                Claims.Add(new Claim(ClaimTypes.Role, userVM.RolesViewModel.strValor));
            }
            var Identity = new ClaimsIdentity(Claims, DefaultAuthenticationTypes.ApplicationCookie);
            IAuthenticationManager authenticationManger = HttpContext.GetOwinContext().Authentication;
            authenticationManger.SignIn(new AuthenticationProperties() { IsPersistent = rememberMe }, Identity);

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Url.Action("Create", "Ocupaciones");
            }
            Result = Redirect(returnUrl);
            return Result;
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (db.Delete(id))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("error_borrado", "El usuario no se pudo borrar");
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    string mensaje = ex.Message;
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult TestAjaxRoles()
        {
            ViewBag.RolesId = new SelectList(rdb.GetRoles(), "id", "strValor");
            ViewBag.UsuariosId = new SelectList("");
            return View();
        }

        public JsonResult GetUsuariosByRoleId(int id)
        {
            var usuariosRoles = this.db.GetUsuariosByIdRol(id);
            List<UsuarioViewModel> usuarios = new List<UsuarioViewModel>();
            foreach (var u in usuariosRoles)
            {
                var usuarioVM = new UsuarioViewModel
                {
                    email = u.Usuarios.email,
                    contrasenia = u.Usuarios.contrasenia,
                    Id = u.Usuarios.Id
                };
                usuarios.Add(usuarioVM);
            }
            return Json(usuarios, JsonRequestBehavior.AllowGet);
        }
    }
}