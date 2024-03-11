namespace GerenciadorDeProjetos.Infrastructure.Interfaces;

public interface ITarefasService
{
    Task<(bool IsSuccess, IEnumerable<Domain.Tarefa>? Tarefas, string? ErrorMessage)> GetTarefasAsync(int projetoId);
}
