namespace GerenciadorDeProjetos.Domain;

public class TarefaHistorico
{
    public int Id { get; set; }
    public int TarefaId { get; set; }
    public string HistoricoJson { get; set; }
    public int UsuarioId { get; set; }
    public DateTime Data { get; set; }

    public TarefaHistorico(int tarefaId, string historicoJson, int usuarioId, DateTime data)
    {
        TarefaId = tarefaId;
        HistoricoJson = historicoJson;
        UsuarioId = usuarioId;
        Data = data;
    }
}
