using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace inmobiliariaPestchanker.Models
{
 
	 
	public class Propietario
    {

// Constructor para pasar a PropietarioView

public Propietario(){}
public Propietario(PropietarioView p)
{
	this.Apellido = p.Apellido;
	this.Dni = p.Dni;
	this.Email = p.Email;
	this.Nombre = p.Nombre;
	this.Telefono = p.Telefono;
	this.Id = p.Id;

}



        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }
		[Required]
		public string Nombre { get; set; }
		[Required]
		public string Apellido { get; set; }
		[Required]
		public string Dni { get; set; }
		public string Telefono { get; set; }
		[Required, EmailAddress]
		public string Email { get; set; }
		[Required]
		public string Clave { get; set; }
		


	    public override string ToString()
    {
        return Apellido + ", " + Nombre;
    }
       
	}
	
	
}
