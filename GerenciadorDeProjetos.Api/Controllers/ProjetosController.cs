using GerenciadorDeProjetos.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeProjetos.Api.Controllers;

[ApiController]
[Route("api/projetos")]
public class ProjetosController : ControllerBase
{
    private readonly IProjetosProvider projetosProvider;

    public ProjetosController(IProjetosProvider projetosProvider)
    {
        this.projetosProvider = projetosProvider;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjetosAsync(int id)
    {
        var result = await projetosProvider.GetProjetosAsync(id);
        if (result.IsSuccess)
        {
            return Ok(result.Projetos);
        }
        return NotFound();
    }
}
