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
        SeedData();
    }

    private void SeedData()
    {
        if (!dbContext.TarefasComentarios.Any())
        {
            dbContext.TarefasComentarios.Add(new Db.TarefaComentario() { Id = 1, TarefaId = 1, UsuarioId = 3, Comentario = "Comentário 1", Data = DateTime.Now });
            dbContext.TarefasComentarios.Add(new Db.TarefaComentario() { Id = 2, TarefaId = 1, UsuarioId = 3, Comentario = "Comentário 2", Data = DateTime.Now });
            dbContext.TarefasComentarios.Add(new Db.TarefaComentario() { Id = 3, TarefaId = 1, UsuarioId = 2, Comentario = "Comentário 3", Data = DateTime.Now });
            dbContext.TarefasComentarios.Add(new Db.TarefaComentario() { Id = 4, TarefaId = 1, UsuarioId = 1, Comentario = "Comentário 4", Data = DateTime.Now });
            dbContext.SaveChanges();
        }
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
