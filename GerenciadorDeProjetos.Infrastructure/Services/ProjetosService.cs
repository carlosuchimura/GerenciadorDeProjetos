﻿using AutoMapper;
using GerenciadorDeProjetos.Domain;
using GerenciadorDeProjetos.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GerenciadorDeProjetos.Infrastructure.Services;

public class ProjetosService : IProjetosService
{
    private readonly Context.Context dbContext;
    private readonly ILogger<ProjetosService> projetosService;
    private readonly IMapper mapper;

    public ProjetosService(Context.Context dbContext, ILogger<ProjetosService> projetosService, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.projetosService = projetosService;
        this.mapper = mapper;
        SeedDataUtil.SeedData(dbContext);
    }

    public async Task<(bool IsSuccess, Domain.Projeto? Projeto, string? ErrorMessage)> GetProjetoAsync(int id)
    {
        try
        {
            var projeto = await dbContext.Projetos.FirstOrDefaultAsync(p => p.Id == id);
            if (projeto != null)
            {
                return (true, mapper.Map<Db.Projeto, Domain.Projeto>(projeto), null);
            }
            return (false, null, "Não encontrado");
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }

    public async Task<(bool IsSuccess, IEnumerable<Domain.Projeto>? Projetos, string? ErrorMessage)> GetProjetosAsync()
    {
        try
        {
            var projetos = await dbContext.Projetos.ToListAsync();
            if (projetos.Any())
            {
                var mappedProjetos = mapper.Map<IEnumerable<Db.Projeto>, IEnumerable<Domain.Projeto>>(projetos);
                return (true, mappedProjetos, null);
            }
            return (false, null, "Projetos não encontrados");
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }
}
