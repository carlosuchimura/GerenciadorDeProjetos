namespace GerenciadorDeProjetos.Domain;

public class Tarefa
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public StatusTarefa Status { get; set; }
    public int Estimativa { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
}

public enum StatusTarefa
{
    Pendente = 0,
    EmAndamento = 1,
    Concluida = 2
}