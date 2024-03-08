using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeProjetos.Api.Db;

public class ProjetoDbContext : DbContext
{
    public DbSet<Projeto> Projetos { get; set; }

    public ProjetoDbContext(DbContextOptions options) : base(options)
    {
    }
}
