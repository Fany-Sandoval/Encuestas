using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Encuestas.Models
{
    public class PerfilesViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        [MinLength(2, ErrorMessage = "El dato no es un nombre")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Solo son aceptadas letras")]
        public String Nombre { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        [MinLength(4, ErrorMessage = "El dato no es un apellido")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Solo son aceptadas letras")]
        public String ApellidoPaterno { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        [MinLength(4, ErrorMessage = "El dato no es un apellido")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Solo son aceptadas letras")]
        public String ApellidoMaterno { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        [Range(14, 150, ErrorMessage = "Tienes que tener una edad aceptada")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo se aceptan números")]
        public int Edad { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime FechaNacimiento { get; set; }
        //[Required(ErrorMessage = "Se necesita de una fotografía")]
        public byte[] Foto { get; set; }
        public int IdUser { get; set; }
    }
}