namespace GerenciadorDeProjetos.Infrastructure.Interfaces;

public interface ITarefasHistoricoService
{
    Task<(bool IsSuccess, IEnumerable<Domain.TarefaHistorico>? TarefasHistorico, string? ErrorMessage)> GetHistoricoAsync(int tarefaId);
}
