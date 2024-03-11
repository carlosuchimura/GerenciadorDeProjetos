using AutoMapper;
using GerenciadorDeProjetos.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GerenciadorDeProjetos.Infrastructure.Services;

public class TarefasHistoricoService : ITarefasHistoricoService
{
    private readonly Context.Context dbContext;
    private readonly ILogger<TarefasHistoricoService> tarefasHistoricoService;
    private readonly IMapper mapper;

    public TarefasHistoricoService(Context.Context dbContext, ILogger<TarefasHistoricoService> tarefasHistoricoService, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.tarefasHistoricoService = tarefasHistoricoService;
        this.mapper = mapper;
        SeedData();
    }

    private void SeedData()
    {
        if (!dbContext.TarefasHistorico.Any())
        {
            dbContext.TarefasHistorico.Add(new Db.TarefaHistorico() { Id = 1, TarefaId = 1, UsuarioId = 3, HistoricoJson = String.Empty, Data = DateTime.Now });
            dbContext.TarefasHistorico.Add(new Db.TarefaHistorico() { Id = 2, TarefaId = 1, UsuarioId = 3, HistoricoJson = String.Empty, Data = DateTime.Now });
            dbContext.TarefasHistorico.Add(new Db.TarefaHistorico() { Id = 3, TarefaId = 1, UsuarioId = 2, HistoricoJson = String.Empty, Data = DateTime.Now });
            dbContext.TarefasHistorico.Add(new Db.TarefaHistorico() { Id = 4, TarefaId = 1, UsuarioId = 1, HistoricoJson = String.Empty, Data = DateTime.Now });
            dbContext.SaveChanges();
        }
    }

    public async Task<(bool IsSuccess, IEnumerable<Domain.TarefaHistorico>? TarefasHistorico, string? ErrorMessage)> GetHistoricoAsync(int tarefaId)
    {
        try
        {
            var historicos = await dbContext.TarefasHistorico.Where(h=> h.TarefaId == tarefaId).ToListAsync();
            if (historicos.Any())
            {
                var mappedHistoricos = mapper.Map<IEnumerable<Db.TarefaHistorico>, IEnumerable<Domain.TarefaHistorico>>(historicos);
                return (true, mappedHistoricos, null);
            }
            return (false, null, "Históricos não encontrados");
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }
}
