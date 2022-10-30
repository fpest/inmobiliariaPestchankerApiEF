
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inmobiliariaPestchanker.Models;
using ProyectoInmobiliaria.Models;

namespace ProyectoInmobiliariaLab3.Controllers;

    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
 
        }
     
    //public DbSet<Propietario> Propietario { get; set; }
    public DbSet<Propietario> Propietario { get; set; }
    public DbSet<Inmueble> Inmueble { get; set; }
    public DbSet<InmuebleView> InmuebleView { get; set; }
    public DbSet<TipoInmueble> TipoInmueble { get; set; }
    public DbSet<Inquilino> Inquilino { get; set; }
    public DbSet<Contrato> Contrato { get; set; }
      
 
    }

