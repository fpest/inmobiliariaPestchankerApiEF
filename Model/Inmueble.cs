using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProyectoInmobiliaria.Models;

namespace inmobiliariaPestchanker.Models
{
    public class Inmueble
    {


public Inmueble() {}

/*
public Inmueble(String Direccion, 
                String TipoUso,
                int TipoInmuebleId, 
                int CantidadAmbientes,
                decimal PrecioInmueble,
                int PropietarioId,
                Boolean Disponible,
                String Imagen,
                decimal CoordenadaE,
                decimal CoordenadaN)

{   this.Direccion = Direccion;
    this.TipoUso = TipoUso;
    this.TipoInmuebleId = TipoInmuebleId;
    this.PrecioInmueble = PrecioInmueble;
    this.PropietarioId = PropietarioId;
    this.Disponible = Disponible;
    this.CantidadAmbientes = CantidadAmbientes;
    this.Imagen = Imagen;
    this.CoordenadaE = CoordenadaE;
    this.CoordenadaN = CoordenadaN;
}


public Inmueble(InmuebleView i)
{
	this.Id = i.Id;
    this.CantidadAmbientes = i.CantidadAmbientes;
    this.CoordenadaE = i.CoordenadaE;
    this.CoordenadaN = i.CoordenadaN;
    this.Direccion = i.Direccion;
    this.Disponible = i.Disponible;
    this.TipoUso = i.TipoUso;
    this.TipoInmuebleId = i.TipoInmuebleId;
    this.PropietarioId = i.PropietarioId;
    this.Imagen = i.Imagen;
    this.PrecioInmueble = i.PrecioInmueble;
}
*/


        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }
		[Required]
        public int PropietarioId { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public decimal CoordenadaN { get; set; }
		[Required]
		public decimal CoordenadaE { get; set; }
        [Required]
        public int TipoInmuebleId { get; set; }
		[Required]
        public string TipoUso { get; set; }
        [Required]
        public int CantidadAmbientes { get; set; }
		[Required]
		public decimal PrecioInmueble { get; set; }
		[Required]
        public Boolean Disponible { get; set; }
        [Required]
	    public string Imagen { get; set; }

        [ForeignKey(nameof(TipoInmuebleId))]
        public TipoInmueble? TipoInmueble { get; set; }
        
        [ForeignKey(nameof(PropietarioId))]
        public Propietario? Propietario { get; set; }
        
		       
    
}}
