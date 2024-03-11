using AutoMapper;
using GerenciadorDeProjetos.Domain;
using GerenciadorDeProjetos.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GerenciadorDeProjetos.Infrastructure.Services;

public class TarefasComentariosService : ITarefasComentariosService
{
    private readonly Context.Context dbContext;
    private readonly ILogger<TarefasComentariosService> tarefasComentariosService;
    private readonly IMapper mapper;

    public TarefasComentariosService(Context.Context dbContext, ILogger<TarefasComentariosService> tarefasComentariosService, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.tarefasComentariosService = tarefasComentariosService;
        this.mapper = mapper;
        SeedDataUtil.SeedData(dbContext);
    }

    public async Task<(bool IsSuccess, IEnumerable<Domain.TarefaComentario>? TarefaComentarios, string? ErrorMessage)> GetComentariosAsync(int tarefaId)
    {
        try
        {
            var comentarios = await dbContext.TarefasComentarios.Where(h=> h.TarefaId == tarefaId).ToListAsync();
            if (comentarios.Any())
            {
                var mappedComentarios = mapper.Map<IEnumerable<Db.TarefaComentario>, IEnumerable<Domain.TarefaComentario>>(comentarios);
                return (true, mappedComentarios, null);
            }
            return (false, null, "Comentários não encontrados");
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, string? ErrorMessage)> CreateComentarioAsync(TarefaComentario tarefaComentario)
    {
        try
        {
            await dbContext.TarefasComentarios.AddAsync(mapper.Map<TarefaComentario, Db.TarefaComentario>(tarefaComentario));
            dbContext.SaveChanges();

            return (true, "Comentário Cadastrado");
        }
        catch (Exception ex)
        {
            return (false, ex.Message);
        }
    }
}
