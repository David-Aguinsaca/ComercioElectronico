using ComercioElectronico.Application;
using ComercioElectronico.Application.Controller;
using ComercioElectronico.Domain.Repository;
using ComercioElectronico.Infraestructure;
using ComercioElectronico.Infraestructure.Controller;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//paso 10
//dbcontext
//builder.Services.AddScoped<ECommerceDbContext>();

//Inyeccion de independencia
builder.Services.AddInfraestructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

/* builder.Services.AddTransient<IBrandRepository, BrandRepository>();
builder.Services.AddTransient<IBrandAppService, BrandAppService>(); */

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
