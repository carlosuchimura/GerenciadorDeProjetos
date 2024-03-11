using GerenciadorDeProjetos.Domain;
using GerenciadorDeProjetos.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeProjetos.Api.Controllers;

[ApiController]
[Route("api/tarefas/relatorio")]
public class RelatorioTarefasController : ControllerBase
{
    private readonly ITarefasService tarefasService;

    public RelatorioTarefasController(ITarefasService tarefasService)
    {
        this.tarefasService = tarefasService;
    }

    [HttpGet("{usuarioId}")]
    public async Task<IActionResult> GetRelatorioAsync(int usuarioId)
    {
        var result = await tarefasService.GetRelatorioAsync(usuarioId);
        if (result.IsSuccess)
        {
            return Ok(result.Relatorio);
        }
        return NotFound();
    }
}
