using AutoMapper;
using GerenciadorDeProjetos.Infrastructure.Db;
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
            dbContext.Projetos.Add(new Db.Projeto() { Id = 1, CustomerId = 1, OrderDate = DateTime.Now, Total = 100 });
            dbContext.Projetos.Add(new Db.Projeto() { Id = 2, CustomerId = 1, OrderDate = DateTime.Now, Total = 200 });
            dbContext.Projetos.Add(new Db.Projeto() { Id = 3, CustomerId = 2, OrderDate = DateTime.Now, Total = 300 });
            dbContext.Projetos.Add(new Db.Projeto() { Id = 4, CustomerId = 2, OrderDate = DateTime.Now, Total = 400 });
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

    public async Task<(bool IsSuccess, IEnumerable<Domain.Projeto>? Projetos, string? ErrorMessage)> GetProjetosAsync(int customerId)
    {
        try
        {
            var projetos = await dbContext.Projetos.Where(o => o.CustomerId == customerId).ToListAsync();
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
