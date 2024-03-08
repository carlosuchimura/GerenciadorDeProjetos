using GerenciadorDeProjetos.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeProjetos.Api.Controllers;

[ApiController]
[Route("api/projetos")]
public class ProjetosController : ControllerBase
{
    private readonly IProjetosService projetosService;

    public ProjetosController(IProjetosService projetosService)
    {
        this.projetosService = projetosService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjetosAsync(int id)
    {
        var result = await projetosService.GetProjetosAsync(id);
        if (result.IsSuccess)
        {
            return Ok(result.Projetos);
        }
        return NotFound();
    }
}
