namespace GerenciadorDeProjetos.Domain;

public class Tarefa
{
    public int Id { get; set; }
    public int ProjetoId { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public PrioridadeTarefa Prioridade { get; set; }
    public StatusTarefa Status { get; set; }
    public int Estimativa { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataConclusao { get; set; }

    public virtual IList<TarefaComentario> Comentarios { get; set; }
    public virtual IList<TarefaHistorico> Historicos { get; set; }

    public Tarefa(string titulo, string descricao, PrioridadeTarefa prioridade, StatusTarefa status, int estimativa, DateTime dataInicio, DateTime? dataConclusao)
    {
        Titulo = titulo;
        Descricao = descricao;
        Prioridade = prioridade;
        Status = status;
        Estimativa = estimativa;
        DataInicio = dataInicio;
        DataConclusao = dataConclusao;
    }
}

public enum StatusTarefa
{
    Pendente = 0,
    EmAndamento = 1,
    Concluida = 2,
    Cancelada = 3
}

public enum PrioridadeTarefa
{
    Baixa = 0,
    Media = 1,
    Alta = 2
}