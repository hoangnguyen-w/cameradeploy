#nullable disable
using CameraBase.Entity;
using CameraBase.IRepository;
using CameraBase.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//chạy local thì lấy IPv4 thay địa chỉ, phát wifi cho máy khác kết nối vào wifi đó rồi Run bài, có link là chạy đc
//builder.WebHost.ConfigureKestrel(options => options.Listen(System.Net.IPAddress.Parse("10.4.67.107D"), 7132));


// Add services to the container.

builder.Services.AddRazorPages();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(
    options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Standard Authorization header uisng the Bearer scheme (\"bearer {token}\")",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        options.OperationFilter<SecurityRequirementsOperationFilter>();
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:TokenSecret").Value)),
        ValidateIssuer = false,
        ValidateAudience = false,
        // ValidIssuer = "mytest.com",
        // ValidAudience = "mytest.com",
    };
});

builder.Services.ConfigureSwaggerGen(setup =>
{/*
    CameraBase / publish / swagger.json*/
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Weather Forecasts",
        Version = "v1"
    });
});


var connectionString = builder.Configuration.GetConnectionString("CameraBasedContextConection") ??
throw new InvalidOperationException("Connection string 'CameraBasedContextConection' not found.");

builder.Services.AddDbContext<CameraBasedContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
{
    //build.WithOrigins("https://localhost:7091");
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICarManarmentRepository, CarManarmentRepository>();
builder.Services.AddScoped<INotifiHistoryRepository, NotifiHistoryRepository>();
builder.Services.AddScoped<ICarLocatorRepository, CarLocatorRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();


var app = builder.Build();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(options =>
//    {
//        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
//        options.RoutePrefix = string.Empty;
//    });
//}

app.UseSwagger();
app.UseSwaggerUI();
/*if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/index.html", "v1");
        options.RoutePrefix = string.Empty;
    });
}*/


app.UseHttpsRedirection();

app.UseCors("MyCors");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
