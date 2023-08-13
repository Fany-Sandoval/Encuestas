using Estadisticas.Modelo;
using Estadisticas.Negocios.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estadisticas.Negocios
{
    public class PerfilesNegocio : IPerfilesNegocio
    {
        private readonly EstadísticasEntities entities = new EstadísticasEntities();

        public bool Insert(Perfiles per)
        {
            bool respuesta = false;
            try
            {
                entities.Perfiles.Add(per);
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

        public List<Perfiles> GetPerfiles()
        {
            return entities.Perfiles.ToList();
        }
    }
}
