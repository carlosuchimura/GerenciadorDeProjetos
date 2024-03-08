namespace GerenciadorDeProjetos.Infrastructure.Profiles;

public class ProjetosProfile : AutoMapper.Profile
{
    public ProjetosProfile()
    {
        CreateMap<Db.Projeto, Domain.Projeto>();
    }
}
