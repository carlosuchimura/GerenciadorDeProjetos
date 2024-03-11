namespace GerenciadorDeProjetos.Domain;

public class TarefaComentario
{
    public int Id { get; set; }
    public int TarefaId { get; set; }
    public string Comentario { get; set; }
    public int UsuarioId { get; set; }
    public DateTime Data { get; set; }
}
