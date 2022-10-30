using System.ComponentModel.DataAnnotations;
using ProyectoInmobiliaria.Models;

namespace inmobiliariaPestchanker.Models
{
    public class TipoInmueble
    {
        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }
        [Required]
        public string Descripcion { get; set; }
       		       
    
}}
