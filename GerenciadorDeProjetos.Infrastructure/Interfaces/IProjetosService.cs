﻿namespace GerenciadorDeProjetos.Infrastructure.Interfaces;

public interface IProjetosService
{
    Task<(bool IsSuccess, IEnumerable<Domain.Projeto>? Projetos, string? ErrorMessage)> GetProjetosAsync(int customerId);
    Task<(bool IsSuccess, Domain.Projeto? Projeto, string? ErrorMessage)> GetProjetoAsync(int id);
}