using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Estadisticas.Modelo;
using Estadisticas.Negocios.Contrato;

namespace Estadisticas.Negocios
{
    public class UsuariosNegocio : IUsuarioNegocio
    {
        private readonly EstadísticasEntities entities = new EstadísticasEntities();
        /// <summary>
        /// Este método se encarga de consultar y validar login
        /// </summary>
        /// <returns></returns>
        public bool ValidarLogin(Usuarios usuario)
        {
            bool respuesta = false;
            try
            {
                var user = entities.Usuarios.
                    Where(u => u.email == usuario.email && u.contrasenia == usuario.contrasenia).FirstOrDefault(); 
                if(user != null)
                {
                    respuesta = true;
                }
                return respuesta;
                /*select * from usuarios where email = ? && contrasenia = ***** */
            }
            catch(Exception ex)
            {
                string error = ex.Message;
                return respuesta;
            }
        }

        public Usuarios Validar(Usuarios usuario)
        {
            Usuarios user = null;
            try
            {
                user = entities.Usuarios.Include("UsuarioRol").
                    Where(p => p.email == usuario.email && p.contrasenia == usuario.contrasenia).FirstOrDefault();
                return user;
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                return user;
            }
        }

        public bool Login(Usuarios usuario)
        {
            bool respuesta = false;
            try
            {
                if (entities.Usuarios.Any(p => p.email == usuario.email && p.contrasenia == usuario.contrasenia))
                {
                    respuesta = true;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return respuesta;
            }
        }

        /// <summary>
        /// Este modelo se encarga de insertar una entidad de tipo usuario
        /// </summary>
        /// <param name="la entidad usuario"></param>
        /// <returns>regresa una respuesta true/false</returns>
        public bool Insert(Usuarios usuario)
        {
            bool respuesta = false;
            try
            {
                entities.Usuarios.Add(usuario);
                entities.SaveChanges();
                respuesta = true;
                return respuesta;
            }
            catch (Exception ex)
            {
                String mensaje = ex.Message;
                return respuesta;
            }
        }

        /// <summary>
        /// Este método se encarga de consultar a todos los usuarios
        /// </summary>
        /// <returns>Lista de usuario</returns>
        public List<Usuarios> GetUsuarios()
        {
            return entities.Usuarios.ToList();
        }

        public bool Delete(int id)
        {
            bool respuesta = false;
            try
            {
                var user = entities.Usuarios.Where(p => p.Id == id).FirstOrDefault();
                entities.Usuarios.Remove(user);
                entities.SaveChanges();
                respuesta = true;
            }
            catch (Exception ex)
            {
                String mensaje = ex.Message;
                return respuesta;
            }
            return respuesta;
        }

        public List<UsuarioRol> GetUsuariosByIdRol(int id)
        {
            Expression<Func<UsuarioRol, bool>> predicate = p => p.IdRol == id;
            var usuarios = entities.UsuarioRol.Include("Usuarios").Where(predicate).ToList();
            return usuarios;
        }
        public bool GetUserById(int id)
        {
            bool respuesta = false;
            try
            {
                var user = entities.Usuarios.Where(p => p.Id == id).FirstOrDefault();
                
                respuesta = true;
            }
            catch (Exception ex)
            {
                String mensaje = ex.Message;
                return respuesta;
            }
            return respuesta;
        }
    }
}
