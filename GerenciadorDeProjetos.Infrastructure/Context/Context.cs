using GerenciadorDeProjetos.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeProjetos.Infrastructure.Context;

public class Context : DbContext
{
    public DbSet<Projeto> Projetos { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<TarefaHistorico> TarefasHistorico { get; set; }
    public DbSet<TarefaComentario> TarefasComentarios { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TarefaComentario>()
            .HasOne(tc => tc.Tarefa)
            .WithMany(t => t.Comentarios)
            .HasForeignKey(tc => tc.TarefaId);

        modelBuilder.Entity<TarefaHistorico>()
            .HasOne(th => th.Tarefa)
            .WithMany(t => t.Historicos)
            .HasForeignKey(th => th.TarefaId);

        base.OnModelCreating(modelBuilder);
    }

    public Context(DbContextOptions options) : base(options)
    {
    }
}
