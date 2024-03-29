﻿using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeProjetos.Infrastructure.Db;

public class Projeto
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Area { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
}

