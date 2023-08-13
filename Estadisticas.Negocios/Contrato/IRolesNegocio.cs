using Estadisticas.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estadisticas.Negocios.Contrato
{
    public interface IRolesNegocio
    {
        bool Insert(Roles rol);

        List<Roles> GetRoles();
    }
}
