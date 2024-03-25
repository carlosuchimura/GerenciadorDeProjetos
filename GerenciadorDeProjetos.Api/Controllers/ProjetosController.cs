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

    [HttpGet]
    public async Task<IActionResult> GetProjetosAsync()
    {
        var result = await projetosService.GetProjetosAsync();
        if (result.IsSuccess)
        {
            return Ok(result.Projetos);
        }
        return NotFound();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjetoAsync(int id)
    {
        var result = await projetosService.GetProjetoAsync(id);
        if (result.IsSuccess)
        {
            return Ok(result.Projeto);
        }
        return NotFound();
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFilesAsync()
    {
        var files = Request.Form.Files;

        foreach (var file in files)
        {
            var content = file.OpenReadStream();


            //var result = await projetosService.UploadFileAsync(file);
            //if (!result.IsSuccess)
            //{
            //    return BadRequest();
            //}
        }

        return Ok();
    }

}
