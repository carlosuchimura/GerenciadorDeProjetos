using GerenciadorDeProjetos.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeProjetos.Api.Controllers;

[ApiController]
[Route("api/historicos")]
public class TarefasHistoricoController : ControllerBase
{
    private readonly ITarefasHistoricoService tarefasHistoricoService;

    public TarefasHistoricoController(ITarefasHistoricoService tarefasHistoricoService)
    {
        this.tarefasHistoricoService = tarefasHistoricoService;
    }

    [HttpGet("{tarefaId}")]
    public async Task<IActionResult> GetHistoricoAsync(int tarefaId)
    {
        var result = await tarefasHistoricoService.GetHistoricoAsync(tarefaId);
        if (result.IsSuccess)
        {
            return Ok(result.TarefasHistorico);
        }
        return NotFound();
    }
}
