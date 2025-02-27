using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>{
    options.Authority = builder.Configuration["IdentityServiceUrl"];
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters.ValidateAudience = false;
    options.TokenValidationParameters.NameClaimType = "username";
});

builder.Services.AddCors(options => {
    options.AddPolicy("customPolicy", b=> {
            b.WithOrigins(builder.Configuration["ClientApp"], "https://app.carsties.com/") // Sadece belirli bir origin için izin vermek
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // Kimlik bilgileri ile istek gönderen istemcilere izin vermek
    });
});

var app = builder.Build();

app.UseCors("customPolicy");

app.MapReverseProxy();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
