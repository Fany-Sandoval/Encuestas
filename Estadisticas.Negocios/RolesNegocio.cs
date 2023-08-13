using Estadisticas.Modelo;
using Estadisticas.Negocios.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estadisticas.Negocios
{
    public class RolesNegocio : IRolesNegocio
    {
        private readonly EstadísticasEntities entities = new EstadísticasEntities();

        public bool Insert(Roles rol)
        {
            bool respuesta = false;
            try
            {
                entities.Roles.Add(rol);
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

        public List<Roles> GetRoles()
        {
            return entities.Roles.ToList();
        }
    }
}
