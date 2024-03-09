using GerenciadorDeProjetos.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeProjetos.Infrastructure.Context;

public class ProjetoDbContext : DbContext
{
    public DbSet<Projeto> Projetos { get; set; }

    public ProjetoDbContext(DbContextOptions options) : base(options)
    {
    }
}
