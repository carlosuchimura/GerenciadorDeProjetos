using GerenciadorDeProjetos.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeProjetos.Api.Controllers;

[ApiController]
[Route("api/usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuariosService usuariosService;

    public UsuariosController(IUsuariosService usuariosService)
    {
        this.usuariosService = usuariosService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsuariosAsync()
    {
        var result = await usuariosService.GetUsuariosAsync();
        if (result.IsSuccess)
        {
            return Ok(result.Usuarios);
        }
        return NotFound();
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsuarioAsync(int id)
    {
        var result = await usuariosService.GetUsuarioAsync(id);
        if (result.IsSuccess)
        {
            return Ok(result.Usuario);
        }
        return NotFound();
    }
}
