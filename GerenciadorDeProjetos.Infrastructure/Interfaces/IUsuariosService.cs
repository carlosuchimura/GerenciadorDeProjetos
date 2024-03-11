namespace GerenciadorDeProjetos.Infrastructure.Interfaces;

public interface IUsuariosService
{
    Task<(bool IsSuccess, IEnumerable<Domain.Usuario>? Usuarios, string? ErrorMessage)> GetUsuariosAsync();
    Task<(bool IsSuccess, Domain.Usuario? Usuario, string? ErrorMessage)> GetUsuarioAsync(int id);
}
