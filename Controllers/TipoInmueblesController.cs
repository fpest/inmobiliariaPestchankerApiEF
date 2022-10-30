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

namespace ProyectoInmobiliariaLab3.Controllers;

[ApiController]
[Route("[controller]")]
//Para autorizar
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

public class TipoInmueblesController : ControllerBase
{
    private readonly DataContext context;
     private readonly IConfiguration config;
    public TipoInmueblesController(DataContext dataContext, IConfiguration config)
        {
            this.context = dataContext;
            this.config = config;
        }


        [HttpGet("ListarTipoInmuebles")]
        // Get Propietario Logueado
		public async Task<IActionResult> ListarTipoInmuebles()
		{
            try
            {
                //List<InmuebleView> liv=new List<InmuebleView>();
				var i = context.TipoInmueble;
                
                //PropietarioView pv = new PropietarioView(p);
                return Ok(i);//
            }
            
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
			
        }





                }



