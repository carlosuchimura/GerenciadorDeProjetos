using GerenciadorDeProjetos.Domain;
using GerenciadorDeProjetos.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeProjetos.Api.Controllers;

[ApiController]
[Route("api/tarefas")]
public class TarefasController : ControllerBase
{
    private readonly ITarefasService tarefasService;

    public TarefasController(ITarefasService tarefasService)
    {
        this.tarefasService = tarefasService;
    }

    [HttpGet("{projetoId}")]
    public async Task<IActionResult> GetProjetoAsync(int projetoId)
    {
        var result = await tarefasService.GetTarefasAsync(projetoId);
        if (result.IsSuccess)
        {
            return Ok(result.Tarefas);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> PostTarefaAsync([FromBody] Tarefa tarefa)
    {
        var result = await tarefasService.CreateTarefaAsync(tarefa);
        if (result.IsSuccess)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpPut]
    public async Task<IActionResult> PutTarefaAsync([FromBody] Tarefa tarefa, int usuarioId)
    {
        var result = await tarefasService.UpdateTarefaAsync(tarefa, usuarioId);
        if (result.IsSuccess)
        {
            return Ok();
        }
        return BadRequest();
    }
}
