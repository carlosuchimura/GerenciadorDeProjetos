namespace GerenciadorDeProjetos.Infrastructure.Interfaces;

public interface ITarefasComentariosService
{
    Task<(bool IsSuccess, IEnumerable<Domain.TarefaComentario>? TarefaComentarios, string? ErrorMessage)> GetComentariosAsync(int tarefaId);
}
