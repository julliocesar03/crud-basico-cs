using AgendaContatosApp.EDs;
using Dapper.FluentMap.Mapping;

namespace AgendaContatosApp.MAPs
{
    public class ContatoMap : EntityMap<ContatoED>
    {
        public ContatoMap() 
        {
            Map(x => x.Codigo).ToColumn("ID");
            Map(x => x.NomeCompleto).ToColumn("NOME");
            Map(x => x.EmailContato).ToColumn("EMAIL");
        }
    }
}
