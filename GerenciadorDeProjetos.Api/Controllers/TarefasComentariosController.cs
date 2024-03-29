﻿using GerenciadorDeProjetos.Domain;
using GerenciadorDeProjetos.Infrastructure.Interfaces;
using GerenciadorDeProjetos.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeProjetos.Api.Controllers;

[ApiController]
[Route("api/comentarios")]
public class TarefasComentariosController : ControllerBase
{
    private readonly ITarefasComentariosService tarefasComentariosService;

    public TarefasComentariosController(ITarefasComentariosService tarefasComentariosService)
    {
        this.tarefasComentariosService = tarefasComentariosService;
    }

    [HttpGet("{tarefaId}")]
    public async Task<IActionResult> GetComentariosAsync(int tarefaId)
    {
        var result = await tarefasComentariosService.GetComentariosAsync(tarefaId);
        if (result.IsSuccess)
        {
            return Ok(result.TarefaComentarios);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> PostComentarioAsync([FromBody] TarefaComentario tarefaComentario)
    {
        var result = await tarefasComentariosService.CreateComentarioAsync(tarefaComentario);
        if (result.IsSuccess)
        {
            return Ok();
        }
        return BadRequest();
    }
}
