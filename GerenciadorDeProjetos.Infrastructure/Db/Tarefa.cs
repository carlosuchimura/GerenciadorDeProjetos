using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeProjetos.Infrastructure.Db;

public class Tarefa
{
    [Key]
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public int Status { get; set; }
    public int Estimativa { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
}