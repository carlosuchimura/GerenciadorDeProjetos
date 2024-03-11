using AutoMapper;
using GerenciadorDeProjetos.Domain;
using GerenciadorDeProjetos.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GerenciadorDeProjetos.Infrastructure.Services;

public class UsuariosService : IUsuariosService
{
    private readonly Context.Context dbContext;
    private readonly ILogger<UsuariosService> usuariosService;
    private readonly IMapper mapper;

    public UsuariosService(Context.Context dbContext, ILogger<UsuariosService> usuariosService, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.usuariosService = usuariosService;
        this.mapper = mapper;
        SeedData();
    }

    private void SeedData()
    {
        if (!dbContext.Usuarios.Any())
        {
            dbContext.Usuarios.Add(new Db.Usuario() { Id = 1, Nome = "Gerente", Email = "gerente@ti.com", Ativo = true });
            dbContext.Usuarios.Add(new Db.Usuario() { Id = 2, Nome = "Analista Senior", Email = "senior@ti.com", Ativo = true });
            dbContext.Usuarios.Add(new Db.Usuario() { Id = 3, Nome = "Estagiario", Email = "estagiario@ti.com", Ativo = true });
            dbContext.SaveChanges();
        }
    }

    public async Task<(bool IsSuccess, IEnumerable<Usuario>? Usuarios, string? ErrorMessage)> GetUsuariosAsync()
    {
        try
        {
            var usuarios = await dbContext.Usuarios.ToListAsync();
            if (usuarios.Any())
            {
                var mappedUsuarios = mapper.Map<IEnumerable<Db.Usuario>, IEnumerable<Domain.Usuario>>(usuarios);
                return (true, mappedUsuarios, null);
            }
            return (false, null, "Usuarios não encontrados");
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, Usuario? Usuario, string? ErrorMessage)> GetUsuarioAsync(int id)
    {
        try
        {
            var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(p => p.Id == id);
            if (usuario != null)
            {
                return (true, mapper.Map<Db.Usuario, Domain.Usuario>(usuario), null);
            }
            return (false, null, "Não encontrado");
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }
}
