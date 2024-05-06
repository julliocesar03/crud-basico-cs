using AgendaContatosApp.EDs;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Dapper;

namespace AgendaContatosApp.RNs
{
    public static class ContatoRN
    {
        public static readonly string connectionString;
        static ContatoRN ()
            {
                connectionString = ConfigurationManager.ConnectionStrings["MinhaConexao"].ConnectionString;
            }

        public static List<ContatoED>? ConsultarTodos()
        {
            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string sql = "SELECT * FROM CONTATO";
                    var contatos = conn.Query<ContatoED>(sql);

                    foreach (var contato in contatos)
                    {
                        Console.WriteLine($"{contato.Codigo} - {contato.NomeCompleto} - {contato.EmailContato}");
                    }

                    return contatos.ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ocorreu um erro ao acessar a base de dados. {e.Message}");
                }
            }
            return null;
        }

        public static ContatoED? ConsultarPorId(int id)
        {
            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    //Abrindo conexao
                    conn.Open();

                    string sql = "SELECT * FROM CONTATO WHERE ID = @id";
                    var contato = conn.Query<ContatoED>(sql, new { id }).FirstOrDefault();

                    if (contato != null)
                    {
                        Console.WriteLine($"{contato.Codigo} - {contato.NomeCompleto} - {contato.EmailContato}");
                    }

                    return contato;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ocorreu um erro ao acessar a base de dados. {e.Message}");
                }
            }
            return null;
        }

        public static void Inserir(ContatoED contato)
        {
            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    //Abrindo conexao
                    conn.Open();

                    string sql = "INSERT INTO CONTATO(NOME, EMAIL) VALUES(@NomeCompleto, @EmailContato)";
                    var qtdLinhas = conn.Execute(sql, contato);

                    if (qtdLinhas <= 0)
                        Console.WriteLine("Nenhum registro foi inserido");
                    else
                        if (qtdLinhas == 1)
                        Console.WriteLine($"Foi inserida {qtdLinhas} linha");

                    else
                        Console.WriteLine($"Foram inseridas {qtdLinhas} linhas");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ocorreu um erro ao acessar a base de dados. {e.Message}");
                }
            }
        }
        public static void Alterar(ContatoED contato)
        {
            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    //Abrindo conexao
                    conn.Open();

                    string sql = "UPDATE CONTATO SET NOME = @NomeCompleto, EMAIL = @EmailContato WHERE ID = @Codigo";
                    var qtdLinhas = conn.Execute(sql, contato);
                    if (qtdLinhas <= 0)
                        Console.WriteLine("Nenhum registro foi alterado");
                    else
                        if (qtdLinhas == 1)
                        Console.WriteLine($"Foi alterada {qtdLinhas} linha");

                    else
                        Console.WriteLine($"Foram alteradas {qtdLinhas} linhas");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ocorreu um erro ao acessar a base de dados. {e.Message}");
                }
            }
        }
        public static void Deletar(int idDeletar)
        {
            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    //Abrindo conexao
                    conn.Open();

                    string sql = "DELETE FROM CONTATO WHERE ID = @idDeletar";
                    var qtdLinhas = conn.Execute(sql, new { idDeletar });
                    if (qtdLinhas <= 0)
                        Console.WriteLine("Nenhum registro foi deletado");
                    else
                        if (qtdLinhas == 1)
                        Console.WriteLine($"Foi deletada {qtdLinhas} linha");

                    else
                        Console.WriteLine($"Foram deletadas {qtdLinhas} linhas");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ocorreu um erro ao acessar a base de dados. {e.Message}");
                }
            }
        }
    }
}
