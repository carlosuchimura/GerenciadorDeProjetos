﻿namespace GerenciadorDeProjetos.Api.Db;

public class Projeto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal Total { get; set; }
}

