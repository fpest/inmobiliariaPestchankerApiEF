using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using inmobiliariaPestchanker.Models;

using ProyectoInmobiliaria.Models;
using System;

namespace ProyectoInmobiliariaLab3.Controllers;

[ApiController]
[Route("[controller]")]
//Para autorizar
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

public class ContratosController : ControllerBase
{
    private readonly DataContext context;
    
     private readonly IConfiguration config;
     private readonly IWebHostEnvironment environment;
    public ContratosController(DataContext dataContext, IConfiguration config, IWebHostEnvironment environment)
        {
            this.context = dataContext;
            this.config = config;
            this.environment = environment;
        }

        
        [HttpGet("ListarContratoInmuebles")]
        // Get Propietario Logueado
		public async Task<IActionResult> ListarContratoInmuebles()
		{
            try
            {
                var id = int.Parse(User.Identity.Name);
             var i = context.Contrato.Where(x => x.Inmueble.PropietarioId==id && x.FechaInicio<= DateTime.Today.Date && x.FechaFin >= DateTime.Today.Date).Select(c => c.Inmueble);

                return Ok(i);//
            }
            
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost("obtenerContrato")]
		public IActionResult obtenerContrato([FromBody] Inmueble i)
            
        {
            try
            {

                 var idProp = int.Parse(User.Identity.Name);
			                
                var inm = context.Contrato
                .Include(inmu => inmu.Inmueble)
                .Include(inqui => inqui.Inquilino)
                .Where(x => x.Inmueble.Id == i.Id).First();
              return Ok(inm);//
            }
            
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

  
        [HttpPost("Registrar")]
        public IActionResult Registrar([FromBody] Inmueble inmueble)
                      
        {
        var idProp = int.Parse(User.Identity.Name);
        inmueble.PropietarioId = idProp;
       
            try
            {
                if (inmueble.Imagen != null )
                    {
                        
                    MemoryStream stream1 = new MemoryStream(Convert.FromBase64String(inmueble.Imagen));
                    IFormFile ImagenInmo = new FormFile(stream1, 0, stream1.Length, "inmueble", ".jpg");
                    string wwwPath = environment.WebRootPath;
                    string path = Path.Combine(wwwPath, "Uploads");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    Random r = new Random();
                    string fileName = "inmueble_" + inmueble.PropietarioId + r.Next(0,100000)+Path.GetExtension(ImagenInmo.FileName);
                    string pathCompleto = Path.Combine(path, fileName);
                    
                    inmueble.Imagen = Path.Combine("http://192.168.100.4:5000/" ,"Uploads/", fileName);
                    using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
                    {
                        ImagenInmo.CopyTo(stream);
                    }

                    context.Inmueble.Add(inmueble);
                    context.SaveChanges();
                    
                    return Ok(inmueble);
                }
                else
                {
                    return BadRequest("Hola ");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        }



