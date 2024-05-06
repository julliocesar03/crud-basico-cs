using System;
using AgendaContatosApp.EDs;
using AgendaContatosApp.MAPs;
using AgendaContatosApp.RNs;
using Dapper.FluentMap;

namespace AgendaContatosApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Inicializar o mapeamento
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new ContatoMap());
            });

            Console.WriteLine("Selecione a operação desejada:");
            Console.WriteLine("1. Consultar todos os contatos");
            Console.WriteLine("2. Consultar contato por ID");
            Console.WriteLine("3. Inserir novo contato");
            Console.WriteLine("4. Atualizar contato existente");
            Console.WriteLine("5. Excluir contato");

            // Ler a opção do usuário
            if (int.TryParse(Console.ReadLine(), out int escolha))
            {
                switch (escolha)
                {
                    case 1:
                        // Consultar todos os contatos
                        var contatos = ContatoRN.ConsultarTodos();
                        // Lógica para manipular os contatos consultados
                        break;
                    case 2:
                        // Consultar contato por ID
                        Console.WriteLine("Digite o ID do contato a ser consultado:");
                        if (int.TryParse(Console.ReadLine(), out int idConsulta))
                        {
                            var contato = ContatoRN.ConsultarPorId(idConsulta);
                            // Lógica para manipular o contato consultado
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                        break;
                    case 3:
                        // Inserir novo contato
                        Console.WriteLine("Digite o nome completo do novo contato:");
                        string nome = Console.ReadLine();
                        Console.WriteLine("Digite o email do novo contato:");
                        string email = Console.ReadLine();

                        var novoContato = new ContatoED()
                        {
                            NomeCompleto = nome,
                            EmailContato = email
                        };

                        ContatoRN.Inserir(novoContato);
                        // Lógica para manipular o resultado da inserção
                        break;
                    case 4:
                        // Atualizar contato existente
                        Console.WriteLine("Digite o ID do contato a ser atualizado:");
                        if (int.TryParse(Console.ReadLine(), out int idAtualizacao))
                        {
                            Console.WriteLine("Digite o novo nome completo:");
                            string novoNome = Console.ReadLine();
                            Console.WriteLine("Digite o novo email:");
                            string novoEmail = Console.ReadLine();

                            var contatoAtualizado = new ContatoED()
                            {
                                Codigo = idAtualizacao,
                                NomeCompleto = novoNome,
                                EmailContato = novoEmail
                            };

                            ContatoRN.Alterar(contatoAtualizado);
                            // Lógica para manipular o resultado da atualização
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                        break;
                    case 5:
                        // Excluir contato
                        Console.WriteLine("Digite o ID do contato a ser excluído:");
                        if (int.TryParse(Console.ReadLine(), out int idExclusao))
                        {
                            ContatoRN.Deletar(idExclusao);
                            // Lógica para manipular o resultado da exclusão
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Opção inválida.");
            }
        }
    }
}
