namespace GerenciadorDeProjetos.Infrastructure.Profiles;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<Db.Projeto, Domain.Projeto>();
        CreateMap<Db.Tarefa, Domain.Tarefa>();
        CreateMap<Db.TarefaComentario, Domain.TarefaComentario>();
        CreateMap<Db.TarefaHistorico, Domain.TarefaHistorico>();
        CreateMap<Db.Usuario, Domain.Usuario>();

        CreateMap<Domain.Tarefa, Db.Tarefa>();
        CreateMap<Domain.TarefaComentario, Db.TarefaComentario>();
        CreateMap<Domain.TarefaHistorico, Db.TarefaHistorico>();
    }
}
