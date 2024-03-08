namespace GerenciadorDeProjetos.Api.Interfaces;

public interface IProjetosProvider
{
    Task<(bool IsSuccess, IEnumerable<Models.Projeto>? Projetos, string? ErrorMessage)> GetProjetosAsync(int customerId);
    Task<(bool IsSuccess, Models.Projeto? Projeto, string? ErrorMessage)> GetProjetoAsync(int id);
}
