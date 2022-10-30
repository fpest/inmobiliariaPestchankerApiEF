//https://localhost:7240/WeatherForecast

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProyectoInmobiliariaLab3.Controllers;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.WebHost.ConfigureKestrel(serverOptions =>
{
	serverOptions.ListenAnyIP(5000);
	serverOptions.ListenAnyIP(5001, listenOptions => listenOptions.UseHttps() );
});



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddCookie(options =>//el sitio web valida con cookie
				{
					options.LoginPath = "/Usuarios/Login";
					options.LogoutPath = "/Usuarios/Logout";
					options.AccessDeniedPath = "/Home/Restringido";
					//options.ExpireTimeSpan = TimeSpan.FromMinutes(5);//Tiempo de expiración
				})
				.AddJwtBearer(options =>//la api web valida con token
				{
					options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = configuration["TokenAuthentication:Issuer"],
						ValidAudience = configuration["TokenAuthentication:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(
							configuration["TokenAuthentication:SecretKey"])),
					};
                  options.Events = new JwtBearerEvents
					{
						OnMessageReceived = context =>
						{
							// Read the token out of the query string
							var accessToken = context.Request.Query["access_token"];
							// If the request is for our hub...
							var path = context.HttpContext.Request.Path;
							if (!string.IsNullOrEmpty(accessToken) &&
								path.StartsWithSegments("/Propietarios/token"))
							{//reemplazar la url por la usada en la ruta ⬆
								context.Token = accessToken;
							}
							return Task.CompletedTask;
						}
					};


				});


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
builder.Services.AddDbContext<DataContext>(
    options => options.UseMySql(
        configuration["ConnectionStrings:DefaultConnection"],
        ServerVersion.AutoDetect(configuration["ConnectionStrings:DefaultConnection"])

    )
);



var app = builder.Build();
app.UseStaticFiles();




//app.UseHttpsRedirection();

app.UseAuthorization();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.MapControllers();

app.Run();
