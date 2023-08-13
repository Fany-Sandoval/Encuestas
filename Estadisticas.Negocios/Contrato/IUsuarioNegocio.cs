using Estadisticas.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estadisticas.Negocios.Contrato
{
    public interface IUsuarioNegocio
    {
        bool ValidarLogin(Usuarios usuario);

        bool Login(Usuarios usuario);

        bool Insert(Usuarios usuario);

        List<Usuarios> GetUsuarios();
        
        Usuarios Validar(Usuarios usuario);
        bool Delete(int id);
        List<UsuarioRol> GetUsuariosByIdRol(int id);
        bool GetUserById(int id);
    }
}
