using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace GerenciadorDeProjetos.Infrastructure.Db;

public class Tarefa
{
    [Key]
    public int Id { get; set; }
    public int ProjetoId { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public int Prioridade { get; set; }
    public int Status { get; set; }
    public int Estimativa { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataConclusao { get; set; }

    public virtual IList<TarefaComentario> Comentarios { get; set; }
    public virtual IList<TarefaHistorico> Historicos { get; set; }
}