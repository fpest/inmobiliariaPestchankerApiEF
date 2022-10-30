using System.ComponentModel.DataAnnotations;
using ProyectoInmobiliaria.Models;

namespace inmobiliariaPestchanker.Models
{
    public class Clave
    {
        [Required]
        public string passwordActual { get; set; }

        [Required]
        public string passwordNueva1 { get; set; }

        [Required]
        public string passwordNueva2 { get; set; }
     
        public string mensaje { get; set; }
       		       
    
}}
