using System.ComponentModel.DataAnnotations;
using inmobiliariaPestchanker.Models;

namespace ProyectoInmobiliaria.Models
{
    public class InmuebleView
        {

public InmuebleView(){}



public InmuebleView(Inmueble i)
	{   this.Id = i.Id;
        this.PropietarioId = i.PropietarioId;
        this.Direccion = i.Direccion;
        this.CoordenadaN = i.CoordenadaN;
        this.CoordenadaE = i.CoordenadaE;
        this.TipoInmuebleId = i.TipoInmuebleId;
        this.TipoUso = i.TipoUso;
        this.CantidadAmbientes = i.CantidadAmbientes;
        this.PrecioInmueble = i.PrecioInmueble;
        this.Disponible = i.Disponible;
        this.Imagen = i.Imagen;
       
}


        [Key]
        [Display(Name = "Código")]
        public int Id { get; set; }
		public int? PropietarioId { get; set; }
        [Required]
        public string Direccion { get; set; }
        public decimal? CoordenadaN { get; set; }
		public decimal? CoordenadaE { get; set; }
        public int? TipoInmuebleId { get; set; }
        [Required]
        public string TipoUso { get; set; }
        [Required]
        public int CantidadAmbientes { get; set; }
		[Required]
		public decimal PrecioInmueble { get; set; }
		[Required]
        public Boolean Disponible { get; set; }
        public Propietario? Duenio { get; set; }
        public string? Imagen { get; set; }
        [Required]
        public string TipoInmueble { get; set; }
        
		       
    
}}
