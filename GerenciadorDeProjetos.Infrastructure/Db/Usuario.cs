﻿using GerenciadorDeProjetos.Domain;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorDeProjetos.Infrastructure.Db;

public class Usuario
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public bool Ativo { get; set; }
    public int Perfil { get; set; }
}
