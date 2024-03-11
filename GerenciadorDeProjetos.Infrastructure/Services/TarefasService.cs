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
        SeedData();
    }

    private void SeedData()
    {
        if (!dbContext.Tarefas.Any())
        {
            dbContext.Tarefas.Add(new Db.Tarefa() { Id = 1, ProjetoId = 1, Titulo = "Tarefa 1", Descricao = "Tarefa 1", Estimativa = 16, Status = (int)StatusTarefa.Pendente, Prioridade = (int)PrioridadeTarefa.Alta, DataInicio = DateTime.Now });
            dbContext.Tarefas.Add(new Db.Tarefa() { Id = 2, ProjetoId = 1, Titulo = "Tarefa 2", Descricao = "Tarefa 2", Estimativa = 16, Status = (int)StatusTarefa.Concluida, Prioridade = (int)PrioridadeTarefa.Baixa, DataInicio = DateTime.Now });
            dbContext.Tarefas.Add(new Db.Tarefa() { Id = 3, ProjetoId = 2, Titulo = "Tarefa 1", Descricao = "Tarefa 1", Estimativa = 16, Status = (int)StatusTarefa.Pendente, Prioridade = (int)PrioridadeTarefa.Media, DataInicio = DateTime.Now });
            dbContext.SaveChanges();
        }

        if (!dbContext.TarefasComentarios.Any())
        {
            dbContext.TarefasComentarios.Add(new Db.TarefaComentario() { Id = 1, TarefaId = 1, UsuarioId = 3, Comentario = "Comentário 1", Data = DateTime.Now });
            dbContext.TarefasComentarios.Add(new Db.TarefaComentario() { Id = 2, TarefaId = 1, UsuarioId = 3, Comentario = "Comentário 2", Data = DateTime.Now });
            dbContext.TarefasComentarios.Add(new Db.TarefaComentario() { Id = 3, TarefaId = 1, UsuarioId = 2, Comentario = "Comentário 3", Data = DateTime.Now });
            dbContext.TarefasComentarios.Add(new Db.TarefaComentario() { Id = 4, TarefaId = 1, UsuarioId = 1, Comentario = "Comentário 4", Data = DateTime.Now });
            dbContext.SaveChanges();
        }

        if (!dbContext.TarefasHistorico.Any())
        {
            dbContext.TarefasHistorico.Add(new Db.TarefaHistorico() { Id = 1, TarefaId = 1, HistoricoJson = String.Empty, Data = DateTime.Now });
            dbContext.TarefasHistorico.Add(new Db.TarefaHistorico() { Id = 2, TarefaId = 1, HistoricoJson = String.Empty, Data = DateTime.Now });
            dbContext.TarefasHistorico.Add(new Db.TarefaHistorico() { Id = 3, TarefaId = 1, HistoricoJson = String.Empty, Data = DateTime.Now });
            dbContext.TarefasHistorico.Add(new Db.TarefaHistorico() { Id = 4, TarefaId = 1, HistoricoJson = String.Empty, Data = DateTime.Now });
            dbContext.SaveChanges();
        }
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
                if(tarefaDb.Prioridade != (int)tarefa.Prioridade)
                {
                    return (false, "Não é possível alterar a prioridade da tarefa");
                }

                if(tarefa.Status == StatusTarefa.Concluida)
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
}
