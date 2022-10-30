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

public class InmueblesController : ControllerBase
{
    private readonly DataContext context;
    
     private readonly IConfiguration config;
     private readonly IWebHostEnvironment environment;
    public InmueblesController(DataContext dataContext, IConfiguration config, IWebHostEnvironment environment)
        {
            this.context = dataContext;
            this.config = config;
            this.environment = environment;
        }

        
        [HttpGet("contipo")]
        public async Task<IActionResult> contipo()
        {
            try
            {
                var id = int.Parse(User.Identity.Name);
               var tipoinmueble = context.TipoInmueble;
              	var inmueblev = context.Inmueble.Include(x => x.TipoInmueble);
                var inmuebles = context.TipoInmueble.Include(e =>  e.Descripcion).Where(e => e.Id ==3 );
                return Ok(inmueblev);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("Actualizar")]
		public IActionResult Actualizar([FromBody] Inmueble inmueble)
        
		{
           

            try
            {
                var id = int.Parse(User.Identity.Name);
                

        var p1 = context.Inmueble.Update(inmueble);
        context.SaveChanges();
                return Ok("sss");//
            }
            
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
    
        }

        [HttpGet("ListarInmuebles")]
        // Get Propietario Logueado
		public async Task<IActionResult> ListarInmuebles()
		{
            try
            {
                var id = int.Parse(User.Identity.Name);
   
				var i = context.Inmueble.Include(x => x.TipoInmueble).Where(a => a.PropietarioId == id);
               return Ok(i);//
            }
            
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
			
        }

        [HttpPost("DatosInmueble")]
		public IActionResult DatosInmueble([FromBody] int id)
            
        {
            try
            {

                 var idProp = int.Parse(User.Identity.Name);
				//var i = context.Inmueble.Find(id);
                 var tipoinmueble = context.TipoInmueble;
              	var i = context.Inmueble.Include(x => x.TipoInmueble).Where(x => x.Id == id && x.PropietarioId == idProp);
              return Ok(i);
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



