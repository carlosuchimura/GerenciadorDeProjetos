﻿using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeProjetos.Infrastructure.Db;

public class TarefaHistorico
{
    [Key]
    public int Id { get; set; }
    public int TarefaId { get; set; }
    public string HistoricoJson { get; set; }
    public int UsuarioId { get; set; }
    public DateTime Data { get; set; }

    public virtual Tarefa Tarefa { get; set; }
}
