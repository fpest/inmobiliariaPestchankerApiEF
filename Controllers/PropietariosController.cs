using MailKit.Net.Smtp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using inmobiliariaPestchanker.Models;
using MimeKit;


namespace ProyectoInmobiliariaLab3.Controllers;

[ApiController]
[Route("[controller]")]
//Para autorizar
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

public class PropietariosController : ControllerBase
{
    private readonly DataContext context;
     private readonly IConfiguration config;
    public PropietariosController(DataContext dataContext, IConfiguration config)
        {
            this.context = dataContext;
            this.config = config;
        }

//actualizarClave

[HttpPost("actualizarClave")]
		public IActionResult actualizarClave([FromBody] Clave clave)
		{
           

            try
            {
                var id = int.Parse(User.Identity.Name);
				Propietario p = context.Propietario.Find(id);

				string hashedActual = Convert.ToBase64String(KeyDerivation.Pbkdf2(
					password: clave.passwordActual,
					salt: System.Text.Encoding.ASCII.GetBytes(config["Salt"]),
					prf: KeyDerivationPrf.HMACSHA1,
					iterationCount: 1000,
					numBytesRequested: 256 / 8));
				
				string hashedNueva = Convert.ToBase64String(KeyDerivation.Pbkdf2(
					password: clave.passwordNueva1,
					salt: System.Text.Encoding.ASCII.GetBytes(config["Salt"]),
					prf: KeyDerivationPrf.HMACSHA1,
					iterationCount: 1000,
					numBytesRequested: 256 / 8));
				
				
                if (hashedActual == p.Clave){

					p.Clave = hashedNueva;
        			var p1 = context.Propietario.Update(p);
        			context.SaveChanges();
					String mensaje = "Clave Actualizada Correctamente.";
					clave.mensaje = mensaje;
                	return Ok(clave);
				}
				else{
					String mensaje = "No se pudo actualizar el Password. Verifique los datos ingresados.";
					clave.mensaje = mensaje;
					return Ok(clave);
				}

				

			}
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
    
			}


        













        [HttpPost("Actualizar")]
		public IActionResult Actualizar([FromBody] Propietario propietario)
		{
           

            try
            {
                var id = int.Parse(User.Identity.Name);
                

           //     Propietario p;// = new Propietario(propietario); 

        var p1 = context.Propietario.Update(propietario);
        context.SaveChanges();
                return Ok("sss");//
            }
            
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
    
        }





        // Get Propietario Logueado
		public IActionResult Get()
		{
            try
            {
                var id = int.Parse(User.Identity.Name);
                Propietario p = context.Propietario.Find(id);
                //PropietarioView pv = new PropietarioView(p);
                return Ok(p);//
            }
            
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
			
        }


		// POST api/<controller>/login
		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<IActionResult> Login([FromBody] LoginView loginView)
		{
			try
			{
				string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
					password: loginView.Clave,
					salt: System.Text.Encoding.ASCII.GetBytes(config["Salt"]),
					prf: KeyDerivationPrf.HMACSHA1,
					iterationCount: 1000,
					numBytesRequested: 256 / 8));
				var p = await context.Propietario.FirstOrDefaultAsync(x => x.Email == loginView.Usuario);
				if (p == null || p.Clave != hashed)//p == null || p.Clave != hashed
				{
					return BadRequest("Nombre de usuario o clave incorrecta");
				}
				else
				{
					var key = new SymmetricSecurityKey(
						System.Text.Encoding.ASCII.GetBytes(config["TokenAuthentication:SecretKey"]));
					var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    
					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, p.Id.ToString()),
				    };

					var token = new JwtSecurityToken(
						issuer: config["TokenAuthentication:Issuer"],
						audience: config["TokenAuthentication:Audience"],
						claims: claims,
						expires: DateTime.Now.AddMinutes(60),
						signingCredentials: credenciales
					);
					return Ok(new JwtSecurityTokenHandler().WriteToken(token));
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


		// GET api/<controller>/5
		[HttpGet("token")]
		
 		public async Task<IActionResult> token()
		{
			try
			{	//este método si tiene autenticación, al entrar, generar clave aleatorio y enviarla por correo
				

				var id = int.Parse(User.Identity.Name);
                Propietario original = context.Propietario.Find(id);
				//Propietario original = await context.Propietario.AsNoTracking().FirstOrDefaultAsync(x => x.Email == perfil.Email);

				var perfil = new
				{
					Email = original.Email,
					Nombre = original.Nombre,
					Rol = "Admin"
				};

				
				Random rand        = new Random(Environment.TickCount);
				string randomChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789";
				string nuevaClave  = "";
				for (int i = 0; i < 8; i++)
				{
					nuevaClave += randomChars[rand.Next(0, randomChars.Length)];
				}
					
						String nuevaClaveSin = nuevaClave;

				nuevaClave = Convert.ToBase64String(KeyDerivation.Pbkdf2(
							password: nuevaClave,
							salt: System.Text.Encoding.ASCII.GetBytes(config["Salt"]),
							prf: KeyDerivationPrf.HMACSHA1,
							iterationCount: 1000,
							numBytesRequested: 256 / 8));

				
				
				original.Clave = nuevaClave;
				context.Propietario.Update(original);	
				await context.SaveChangesAsync();	
				var message = new MimeKit.MimeMessage();
				message.To.Add(new MailboxAddress(perfil.Nombre, perfil.Email));
				message.From.Add(new MailboxAddress(perfil.Nombre, perfil.Email));
	
				message.Subject = "Nueva Password para la Aplicación de la Inmobiliaria Pestchanker";
				message.Body = new TextPart("html")
				{
					Text = @$"<h1>Hola</h1>
					<p>¡Bienvenido, {perfil.Nombre} Clave {nuevaClaveSin}</p>",//Envio
				};
				message.Headers.Add("Encabezado", "Valor");//solo si hace falta
				MailKit.Net.Smtp.SmtpClient client = new SmtpClient();
				client.ServerCertificateValidationCallback = (object sender, 
					System.Security.Cryptography.X509Certificates.X509Certificate certificate, 
					System.Security.Cryptography.X509Certificates.X509Chain chain,
					System.Net.Security.SslPolicyErrors sslPolicyErrors) => { return true; };
				client.Connect("smtp.gmail.com", 465, MailKit.Security.SecureSocketOptions.Auto);
				client.Authenticate(config["SMTPUser"], config["SMTPPass"]);//estas credenciales deben estar en el user secrets
				//client.Authenticate("ulp.api.net@gmail.com", "ktitieuikmuzcuup");
				await client.SendAsync(message);
				return Ok("Su Password fue restaurada.");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}



		// GET api/<controller>/5
		[HttpPost("emailPedido")]
		[AllowAnonymous]
		public async Task<IActionResult> emailPedido([FromBody] string email)
		{
			try
			{	


				var entidad1 = await context.Propietario.FirstOrDefaultAsync(x => x.Email == email);
				var entidad= new PropietarioView(entidad1);
				var key = new SymmetricSecurityKey(
						System.Text.Encoding.ASCII.GetBytes(config["TokenAuthentication:SecretKey"]));
					var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
					var claims = new List<Claim>
					
					
					{
						new Claim(ClaimTypes.Name, entidad1.Id.ToString()),
				    };


					var token = new JwtSecurityToken(
						issuer: config["TokenAuthentication:Issuer"],
						audience: config["TokenAuthentication:Audience"],
						claims: claims,
						expires: DateTime.Now.AddMinutes(600),
						signingCredentials: credenciales
					);
					var to = new JwtSecurityTokenHandler().WriteToken(token);
					
					var direccion = "http://192.168.100.4:5000/Propietarios/token?access_token=" + to;
					try
			{
				
	
				var message = new MimeKit.MimeMessage();
				message.To.Add(new MailboxAddress(entidad.Nombre, entidad1.Email));
				message.From.Add(new MailboxAddress(entidad.Nombre, entidad1.Email));
				message.Subject = "Reseteo de Password";
				message.Body = new TextPart("html")

				
				{
					Text = @$"<h1>Hola</h1>
					<p>Bienvenido, {entidad1.Nombre}  <a href={direccion} >Restablecer</a> </p>",					
				};
				
				


				message.Headers.Add("Encabezado", "Valor");
				MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient();
				client.ServerCertificateValidationCallback = (object sender,
				System.Security.Cryptography.X509Certificates.X509Certificate certificate,
				System.Security.Cryptography.X509Certificates.X509Chain chain,
				System.Net.Security.SslPolicyErrors sslPolicyErrors) => { return true;};
				client.Connect("smtp.gmail.com", 465, MailKit.Security.SecureSocketOptions.Auto);
			client.Authenticate(config["SMTPUser"], config["SMTPPass"]);
	//			client.Authenticate("ulp.api.net@gmail.com", "ktitieuikmuzcuup");

				await client.SendAsync(message);
				return Ok("ok");
			
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
				return entidad != null ? Ok(entidad) : NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

	}










/*

		// GET api/<controller>/5
		[HttpPost("email")]
		[AllowAnonymous]
		public async Task<IActionResult> GetByEmail([FromForm]string email)
		{
			try
			{	//método sin autenticar, busca el propietario xemail
				var entidad = await context.Propietario.FirstOrDefaultAsync(x => x.Email == email);
				//para hacer: si el propietario existe, mandarle un email con un enlace con el token
				//ese enlace servirá para resetear la contraseña
				return entidad != null ? Ok(entidad) : NotFound();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
*/

