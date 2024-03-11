namespace GerenciadorDeProjetos.Domain;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public UsuarioPerfil Perfil { get; set; }
}

public enum UsuarioPerfil
{
    Desenvolvedor = 1,
    Gerente = 2
}
