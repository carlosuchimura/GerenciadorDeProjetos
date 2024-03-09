namespace GerenciadorDeProjetos.Infrastructure.Profiles;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<Db.Projeto, Domain.Projeto>();
        CreateMap<Db.Tarefa, Domain.Tarefa>();
        CreateMap<Db.TarefaHistorico, Domain.TarefaHistorico>();
        CreateMap<Db.Usuario, Domain.Usuario>();
    }
}
