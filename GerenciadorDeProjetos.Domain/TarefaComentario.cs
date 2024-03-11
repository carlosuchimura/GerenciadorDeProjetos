using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciadorDeProjetos.Domain;

public class TarefaComentario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int TarefaId { get; set; }
    public string Comentario { get; set; }
    public int UsuarioId { get; set; }
    public DateTime Data { get; set; }

    public TarefaComentario(int tarefaId, string comentario, int usuarioId, DateTime data)
    {
        TarefaId = tarefaId;
        Comentario = comentario;
        UsuarioId = usuarioId;
        Data = data;
    }
}
