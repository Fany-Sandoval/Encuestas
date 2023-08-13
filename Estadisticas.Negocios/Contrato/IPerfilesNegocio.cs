using Estadisticas.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estadisticas.Negocios.Contrato
{
    public interface IPerfilesNegocio
    {
        bool Insert(Perfiles per);
        List<Perfiles> GetPerfiles();

    }
}
