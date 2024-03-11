using AutoMapper;
using GerenciadorDeProjetos.Domain;
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
        SeedDataUtil.SeedData(dbContext);
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
