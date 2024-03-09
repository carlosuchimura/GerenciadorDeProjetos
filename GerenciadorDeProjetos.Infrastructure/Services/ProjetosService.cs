using AutoMapper;
using GerenciadorDeProjetos.Infrastructure.Context;
using GerenciadorDeProjetos.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GerenciadorDeProjetos.Infrastructure.Services;

public class ProjetosService : IProjetosService
{
    private readonly ProjetoDbContext dbContext;
    private readonly ILogger<ProjetosService> projetosService;
    private readonly IMapper mapper;

    public ProjetosService(ProjetoDbContext dbContext, ILogger<ProjetosService> projetosService, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.projetosService = projetosService;
        this.mapper = mapper;
        SeedData();
    }

    private void SeedData()
    {
        if (!dbContext.Projetos.Any())
        {
            dbContext.Projetos.Add(new Db.Projeto() { Id = 1, Nome = "Teste 1", Area = "TI", DataInicio = DateTime.Now });
            dbContext.Projetos.Add(new Db.Projeto() { Id = 2, Nome = "Teste 2", Area = "TI", DataInicio = DateTime.Now });
            dbContext.Projetos.Add(new Db.Projeto() { Id = 3, Nome = "Teste 3", Area = "TI", DataInicio = DateTime.Now });
            dbContext.Projetos.Add(new Db.Projeto() { Id = 4, Nome = "Teste 4", Area = "TI", DataInicio = DateTime.Now });
            dbContext.SaveChanges();
        }
    }

    public async Task<(bool IsSuccess, Domain.Projeto? Projeto, string? ErrorMessage)> GetProjetoAsync(int id)
    {
        try
        {
            var projeto = await dbContext.Projetos.FirstOrDefaultAsync(p => p.Id == id);
            if (projeto != null)
            {
                return (true, mapper.Map<Db.Projeto, Domain.Projeto>(projeto), null);
            }
            return (false, null, "Não encontrado");
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, IEnumerable<Domain.Projeto>? Projetos, string? ErrorMessage)> GetProjetosAsync()
    {
        try
        {
            var projetos = await dbContext.Projetos.ToListAsync();
            if (projetos.Any())
            {
                var mappedProjetos = mapper.Map<IEnumerable<Db.Projeto>, IEnumerable<Domain.Projeto>>(projetos);
                return (true, mappedProjetos, null);
            }
            return (false, null, "Projetos não encontrados");
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }
}
