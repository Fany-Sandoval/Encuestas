//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Estadisticas.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class Perfiles
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public Nullable<int> Edad { get; set; }
        public Nullable<System.DateTime> FechaNacimiento { get; set; }
        public byte[] Foto { get; set; }
        public int IdUser { get; set; }
    
        public virtual Usuarios Usuarios { get; set; }
    }
}
