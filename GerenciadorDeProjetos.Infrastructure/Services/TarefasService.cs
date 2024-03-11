using AutoMapper;
using GerenciadorDeProjetos.Domain;
using GerenciadorDeProjetos.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace GerenciadorDeProjetos.Infrastructure.Services;

public class TarefasService : ITarefasService
{
    private readonly Context.Context dbContext;
    private readonly ILogger<TarefasService> tarefasService;
    private readonly IMapper mapper;

    public TarefasService(Context.Context dbContext, ILogger<TarefasService> tarefasService, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.tarefasService = tarefasService;
        this.mapper = mapper;
        SeedDataUtil.SeedData(dbContext);
    }

    public async Task<(bool IsSuccess, IEnumerable<Domain.Tarefa>? Tarefas, string? ErrorMessage)> GetTarefasAsync(int projetoId)
    {
        try
        {
            var Tarefas = await dbContext.Tarefas
                .Include(t => t.Comentarios)
                .Include(t => t.Historicos)
                .Where(t => t.ProjetoId == projetoId).ToListAsync();
            if (Tarefas.Any())
            {
                var mappedTarefas = mapper.Map<IEnumerable<Db.Tarefa>, IEnumerable<Domain.Tarefa>>(Tarefas);
                return (true, mappedTarefas, null);
            }
            return (false, null, "Tarefas não encontradas");
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, Domain.Tarefa? Tarefa, string? ErrorMessage)> GetTarefaAsync(int id)
    {
        try
        {
            var Tarefa = await dbContext.Tarefas
                .Include(t => t.Comentarios)
                .Include(t => t.Historicos)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (Tarefa != null)
            {
                return (true, mapper.Map<Db.Tarefa, Domain.Tarefa>(Tarefa), null);
            }
            return (false, null, "Não encontrado");
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, string? ErrorMessage)> CreateTarefaAsync(Tarefa tarefa)
    {
        try
        {
            var qtdeAtividade = dbContext.Tarefas.Where(t => t.ProjetoId == tarefa.ProjetoId).Count();
            if (qtdeAtividade >= 20)
            {
                return (false, "Não é possível criar mais de 20 tarefas para o projeto");
            }

            await dbContext.Tarefas.AddAsync(mapper.Map<Tarefa, Db.Tarefa>(tarefa));
            dbContext.SaveChanges();

            return (true, "Tarefa Cadastrada");
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, string? ErrorMessage)> UpdateTarefaAsync(Tarefa tarefa, int usuarioId)
    {
        try
        {
            var tarefaDb = dbContext.Tarefas.FirstOrDefault(p => p.Id == tarefa.Id);
            if (tarefaDb != null)
            {
                if (tarefaDb.Prioridade != (int)tarefa.Prioridade)
                {
                    return (false, "Não é possível alterar a prioridade da tarefa");
                }

                if (tarefa.Status == StatusTarefa.Concluida)
                {
                    tarefa.DataConclusao = DateTime.Now;
                }

                tarefaDb.Titulo = tarefa.Titulo;
                tarefaDb.Descricao = tarefa.Descricao;
                tarefaDb.Estimativa = tarefa.Estimativa;
                tarefaDb.Status = (int)tarefa.Status;
                tarefaDb.Prioridade = (int)tarefa.Prioridade;
                tarefaDb.DataInicio = tarefa.DataInicio;
                tarefaDb.DataConclusao = tarefa.DataConclusao;

                // Convert tarefaDb to JSON string
                var tarefaDbJson = JsonSerializer.Serialize(tarefaDb);
                var tarefaHistorico = new TarefaHistorico(tarefaDb.Id, tarefaDbJson, usuarioId, DateTime.Now);
                await dbContext.TarefasHistorico.AddAsync(mapper.Map<TarefaHistorico, Db.TarefaHistorico>(tarefaHistorico));

                dbContext.SaveChanges();

                return (true, "Tarefa Atualizada");
            }
            else
            {
                return (false, "Tarefa não encontrada");
            }
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, string? ErrorMessage)> PostHistoricoAsync(TarefaHistorico tarefaHistorico)
    {
        try
        {
            await dbContext.TarefasHistorico.AddAsync(mapper.Map<TarefaHistorico, Db.TarefaHistorico>(tarefaHistorico));
            dbContext.SaveChanges();

            return (true, "Histórico Cadastrado");
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, IEnumerable<RelatorioTarefas> Relatorio, string? ErrorMessage)> GetRelatorioAsync(int usuarioId)
    {
        try
        {
            var usuario = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Id == usuarioId);
            if (usuario == null)
            {
                return (false, null, "Usuário não encontrado");
            }

            if (usuario.Perfil != (int)UsuarioPerfil.Gerente)
            {
                return (false, null, "Usuário não possui acesso");
            }

            var tarefas = await dbContext.Tarefas
                .Where(t => t.DataConclusao >= DateTime.Now.AddDays(-30))
                .GroupBy(t => t.ProjetoId)
                .Select(g => new RelatorioTarefas()
                {
                    ProjetoId = g.Key,
                    QtdeTarefas = g.Count()
                })
                .ToListAsync();

            return (true, tarefas, "Comentário Cadastrado");
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }
}
