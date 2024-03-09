namespace GerenciadorDeProjetos.Domain;

public class TarefaHistorico
{
    public int Id { get; set; }
    public int AtividadeId { get; set; }
    public string HistoricoJson { get; set; }
    public int UsuarioId { get; set; }
    public DateTime Data { get; set; }
}
