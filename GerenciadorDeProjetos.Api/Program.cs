using GerenciadorDeProjetos.Infrastructure.Context;
using GerenciadorDeProjetos.Infrastructure.Interfaces;
using GerenciadorDeProjetos.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>(options =>
{
    options.UseInMemoryDatabase("GerenciadorDeProjetos");
});

builder.Services.AddScoped<IProjetosService, ProjetosService>();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<ITarefasService, TarefasService>();
builder.Services.AddScoped<ITarefasComentariosService, TarefasComentariosService>();
builder.Services.AddScoped<ITarefasHistoricoService, TarefasHistoricoService>();

builder.Services.AddAutoMapper(typeof(Program));

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
