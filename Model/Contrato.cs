using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace inmobiliariaPestchanker.Models
{
    public class Contrato
    {
        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }
		
		[Required]
		public DateTime FechaInicio { get; set; }
		[Required]
		public DateTime FechaFin { get; set; }
		[Required]
		public decimal Precio { get; set; }
		[Required]
		public Inquilino Inquilino { get; set; }
        [Required]
		public Inmueble Inmueble { get; set; }
        
       
       
	}
}