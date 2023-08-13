using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Encuestas.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio"), StringLength(60, MinimumLength = 10)]
        [DataType(DataType.EmailAddress, ErrorMessage = "El campo tiene que ser un correo")]
        public string email { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio"), StringLength(60, MinimumLength = 6, ErrorMessage = "Debe de contener una contraseña valida")]
        public string contrasenia { get; set; }


        public RolesViewModel RolesViewModel { get; set; }
    }
}