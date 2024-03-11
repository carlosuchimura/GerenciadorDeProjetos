using GerenciadorDeProjetos.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeProjetos.Infrastructure.Services;

public static class SeedDataUtil
{
    public static void SeedData(Context.Context dbContext)
    {
        if (!dbContext.Projetos.Any())
        {
            dbContext.Projetos.Add(new Db.Projeto() { Id = 1, Nome = "Projeto 1", Area = "TI", DataInicio = DateTime.Now });
            dbContext.Projetos.Add(new Db.Projeto() { Id = 2, Nome = "Projeto 2", Area = "TI", DataInicio = DateTime.Now });
            dbContext.Projetos.Add(new Db.Projeto() { Id = 3, Nome = "Projeto 3", Area = "TI", DataInicio = DateTime.Now });
            dbContext.Projetos.Add(new Db.Projeto() { Id = 4, Nome = "Projeto 4", Area = "TI", DataInicio = DateTime.Now });
            dbContext.SaveChanges();
        }

        if (!dbContext.Tarefas.Any())
        {
            dbContext.Tarefas.Add(new Db.Tarefa() { Id = 1, ProjetoId = 1, Titulo = "Tarefa 1", Descricao = "Tarefa 1", Estimativa = 16, Status = (int)StatusTarefa.Pendente, Prioridade = (int)PrioridadeTarefa.Alta, DataInicio = DateTime.Now });
            dbContext.Tarefas.Add(new Db.Tarefa() { Id = 2, ProjetoId = 1, Titulo = "Tarefa 2", Descricao = "Tarefa 2", Estimativa = 16, Status = (int)StatusTarefa.EmAndamento, Prioridade = (int)PrioridadeTarefa.Baixa, DataInicio = DateTime.Now });
            dbContext.Tarefas.Add(new Db.Tarefa() { Id = 3, ProjetoId = 2, Titulo = "Tarefa 1", Descricao = "Tarefa 1", Estimativa = 16, Status = (int)StatusTarefa.Pendente, Prioridade = (int)PrioridadeTarefa.Media, DataInicio = DateTime.Now });
            dbContext.Tarefas.Add(new Db.Tarefa() { Id = 4, ProjetoId = 3, Titulo = "Tarefa 1", Descricao = "Tarefa 1", Estimativa = 16, Status = (int)StatusTarefa.Concluida, Prioridade = (int)PrioridadeTarefa.Alta, DataInicio = DateTime.Now, DataConclusao = DateTime.Now.AddDays(-15) });
            dbContext.Tarefas.Add(new Db.Tarefa() { Id = 5, ProjetoId = 3, Titulo = "Tarefa 2", Descricao = "Tarefa 2", Estimativa = 16, Status = (int)StatusTarefa.Concluida, Prioridade = (int)PrioridadeTarefa.Baixa, DataInicio = DateTime.Now, DataConclusao = DateTime.Now.AddDays(-20) });
            dbContext.Tarefas.Add(new Db.Tarefa() { Id = 6, ProjetoId = 4, Titulo = "Tarefa 1", Descricao = "Tarefa 1", Estimativa = 16, Status = (int)StatusTarefa.Concluida, Prioridade = (int)PrioridadeTarefa.Media, DataInicio = DateTime.Now, DataConclusao = DateTime.Now.AddDays(-22) });
            dbContext.SaveChanges();
        }

        if (!dbContext.TarefasComentarios.Any())
        {
            dbContext.TarefasComentarios.Add(new Db.TarefaComentario() { Id = 1, TarefaId = 1, UsuarioId = 3, Comentario = "Comentário 1", Data = DateTime.Now });
            dbContext.TarefasComentarios.Add(new Db.TarefaComentario() { Id = 2, TarefaId = 1, UsuarioId = 3, Comentario = "Comentário 2", Data = DateTime.Now });
            dbContext.TarefasComentarios.Add(new Db.TarefaComentario() { Id = 3, TarefaId = 1, UsuarioId = 2, Comentario = "Comentário 3", Data = DateTime.Now });
            dbContext.TarefasComentarios.Add(new Db.TarefaComentario() { Id = 4, TarefaId = 1, UsuarioId = 1, Comentario = "Comentário 4", Data = DateTime.Now });
            dbContext.SaveChanges();
        }

        if (!dbContext.TarefasHistorico.Any())
        {
            dbContext.TarefasHistorico.Add(new Db.TarefaHistorico() { Id = 1, TarefaId = 1, HistoricoJson = String.Empty, Data = DateTime.Now });
            dbContext.TarefasHistorico.Add(new Db.TarefaHistorico() { Id = 2, TarefaId = 1, HistoricoJson = String.Empty, Data = DateTime.Now });
            dbContext.TarefasHistorico.Add(new Db.TarefaHistorico() { Id = 3, TarefaId = 1, HistoricoJson = String.Empty, Data = DateTime.Now });
            dbContext.TarefasHistorico.Add(new Db.TarefaHistorico() { Id = 4, TarefaId = 1, HistoricoJson = String.Empty, Data = DateTime.Now });
            dbContext.SaveChanges();
        }

        if (!dbContext.Usuarios.Any())
        {
            dbContext.Usuarios.Add(new Db.Usuario() { Id = 1, Nome = "Gerente", Email = "gerente@ti.com", Perfil = 2, Ativo = true });
            dbContext.Usuarios.Add(new Db.Usuario() { Id = 2, Nome = "Analista Senior", Email = "senior@ti.com", Perfil = 1, Ativo = true });
            dbContext.Usuarios.Add(new Db.Usuario() { Id = 3, Nome = "Estagiario", Email = "estagiario@ti.com", Perfil = 1, Ativo = true });
            dbContext.SaveChanges();
        }
    }
}
