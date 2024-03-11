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
}
