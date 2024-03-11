using GerenciadorDeProjetos.Domain;

namespace GerenciadorDeProjetos.Infrastructure.Interfaces;

public interface ITarefasService
{
    Task<(bool IsSuccess, IEnumerable<Domain.Tarefa>? Tarefas, string? ErrorMessage)> GetTarefasAsync(int projetoId);

    Task<(bool IsSuccess, string? ErrorMessage)> CreateTarefaAsync(Tarefa tarefa);

    Task<(bool IsSuccess, string? ErrorMessage)> UpdateTarefaAsync(Tarefa tarefa, int usuarioId);
}
