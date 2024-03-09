namespace GerenciadorDeProjetos.Domain;

public class Projeto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Area { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
}
