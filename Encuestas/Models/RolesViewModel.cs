using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Encuestas.Models
{
    public class RolesViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Se requiere de este campo")]
        [MinLength(5, ErrorMessage = "El dato puesto no puede ser un rol")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Solo son aceptadas letras")]
        public string strValor { get; set; }
    }
}