namespace GerenciadorDeProjetos.Api.Profiles;

public class ProjetosProfile : AutoMapper.Profile
{
    public ProjetosProfile()
    {
        CreateMap<Db.Projeto, Models.Projeto>();
    }
}
