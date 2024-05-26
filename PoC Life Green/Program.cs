using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace POC_PIM
{

    public class Usuario // classe para para cadastro de usuario.
    {
        public int Id { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

    }
    public class Produto
    {
        [Key] // PRIMARY KEY
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public string TipoProduto { get; set; }
    }

    public class contexto : DbContext // CLASSE PARA AS TABELAS 
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;");
        }
    }

    class Program
    {

        static void Main(string[] args) // FUNÇÃO PRINCIPAL
        {
            using (var contexto = new contexto())
            {
                contexto.Database.EnsureCreated();
            }

            while (true)
            {
                Console.WriteLine("                                                                                                                                          versão 1.0 (ESN)\n");
                Console.WriteLine("                                          ****************************************************\n");
                Console.WriteLine("                                         ******                                          ****** \n");
                Console.WriteLine("                                         *********    Sistema de cadastro Life Green    *********\n");
                Console.WriteLine("                                         ******                                          ****** \n");
                Console.WriteLine("                                          ****************************************************  \n");
                Console.WriteLine("\n1.Cadastrar usuário.");
                Console.WriteLine("\n2.Cadastrar produto.");
                Console.WriteLine("\n3.Listar usuários cadastrados.");
                Console.WriteLine("\n4.Listar produtos cadastrados.");
                Console.WriteLine("\n5.Excluir usuário.");
                Console.WriteLine("\n6.Excluir produto.");
                Console.WriteLine("\n7.Fechar/sair.");
                Console.WriteLine("\nEscolha uma opeção para seguir: ");
                string opcao = Console.ReadLine();
                switch (opcao)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("                                                                                                                                          versão 1.0 (ESN)\n");
                        Console.WriteLine("Cadastro de Usuarios\n");
                        string nome, email, telefone;
                        do
                        {
                            Console.Write("Digite o nome do usuário: ");
                            nome = LerApenasLetras();
                        } while (string.IsNullOrWhiteSpace(nome));
                        do
                        {
                            Console.Write("Digite o E-mail do usuário: ");
                            email = Console.ReadLine();
                        } while (string.IsNullOrWhiteSpace(email));
                        do
                        {
                            Console.Write("Insira o Telefone do usuário: ");
                            telefone = LerApenasNumeros();
                        } while (string.IsNullOrWhiteSpace(telefone));

                        SalvarUsuario(nome, email, telefone);

                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("                                                                                                                                          versão 1.0 (ESN)\n");
                        Console.WriteLine("Cadastro de Produtos\n");
                        string item, categoria;
                        do
                        {
                            Console.Write("Digite o nome do produto a ser cadastrado: ");
                            item = LerApenasLetras();
                        } while (string.IsNullOrWhiteSpace(item));
                        do
                        {
                            Console.Write("Digite qual a categoria do produto (Frutas, Vegetais, Legumes): ");
                            categoria = LerApenasLetras();
                        } while (string.IsNullOrWhiteSpace(categoria));

                        SalvarProduto(item, categoria);

                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("                                                                                                                                          versão 1.0 (ESN)\n");
                        Usuarioscadastrados();
                        break;

                    case "4":
                        Console.Clear();
                        Console.WriteLine("                                                                                                                                          versão 1.0 (ESN)\n");
                        ProdutosCadastrados();
                        break;

                    case "5":
                        Console.Clear();
                        Console.WriteLine("                                                                                                                                          versão 1.0 (ESN)\n");
                        Console.WriteLine("Excluir Usuario\n");
                        ApagarUsuario();
                        break;

                    case "6":
                        Console.Clear();
                        Console.WriteLine("                                                                                                                                          versão 1.0 (ESN)\n");
                        Console.WriteLine("Excluir Produtos\n");
                        ApagarProduto();
                        break;

                    case "7":
                        Console.Clear();
                        Console.WriteLine("                                                                                                                                          versão 1.0 (ESN)\n");
                        Console.WriteLine("\nBY ESN(Extreme Security Network)");
                        Console.WriteLine("\nFechando o sistema....");
                        return;

                    default:
                        Console.WriteLine("\nOpção invalida!");
                        break;
                }
                Console.WriteLine("\nPrecione Enter para continuar");
                Console.ReadLine();
                Console.Clear();
            }
        }

        static void SalvarUsuario(string nome, string email, string telefone) // FUNÇÃO PARA SALVAR OS DADOS INSERIDOS NA TBELA USUARIO
        {
            using (var contexto = new contexto())
            {
                var usuario = new Usuario { NomeUsuario = nome, Email = email, Telefone = telefone };
                contexto.Usuarios.Add(usuario);
                contexto.SaveChanges();
                Console.WriteLine("Usuario cadastrado com sucesso!");
            }
        }

        static void SalvarProduto(string item, string categoria) // FANÇÃO PARA SALVAR OS DADOS INSERIDOS NA TABELA PRODUTOS
        {
            using (var contexto = new contexto())
            {
                var produto = new Produto { NomeProduto = item, TipoProduto = categoria };
                contexto.Produtos.Add(produto);
                contexto.SaveChanges();
                Console.WriteLine("Produto cadastrado com sucesso!");
            }
        }

        static void Usuarioscadastrados() //FUNÇÃO PARA LISTAGEM DE USUARIOS
        {
            using (var contexto = new contexto())
            {
                var usuarios = contexto.Usuarios;
                Console.WriteLine("\nLista de usuários cadastrados\n");
                foreach (var usuario in usuarios)
                {
                    Console.WriteLine($"ID: {usuario.Id}, Nome: {usuario.NomeUsuario}, E-mail: {usuario.Email}, Telefone: {usuario.Telefone}\n");
                }
            }
        }

        static void ProdutosCadastrados() // FUNÇÃO PÁRA LISTAGEM DE PRODUTOS
        {
            using (var contexto = new contexto())
            {
                var produtos = contexto.Produtos;
                Console.WriteLine("\nLista de produtos cadastrados\n");
                foreach (var produto in produtos)
                {
                    Console.WriteLine($"ID: {produto.IdProduto}, Nome item: {produto.NomeProduto}, Categoria: {produto.TipoProduto}\n");
                }
            }

        }


        static void ApagarUsuario() // FUNÇÃO PARA DELETAR REGISTROS FEITOS NA TABELA USUARIO
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;";

            Console.WriteLine("Digite o ID que será excluido");
            int objetoId = int.Parse(Console.ReadLine());

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM Usuarios WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", objetoId);

                        int rowsAffcted = command.ExecuteNonQuery();

                        if (rowsAffcted > 0)
                        {
                            Console.WriteLine("Usuario deletado com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Usuario não encontrado..");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }

        }

        static void ApagarProduto() // FUNÇÃO PARA DELETAR REGISTROS FEITOS DA TABELA PRODUTOS 
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;";

            Console.WriteLine("Digite o ID que será excluido");
            int objetoId = int.Parse(Console.ReadLine());

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM Produtos WHERE IdProduto = @IdProduto";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdProduto", objetoId);

                        int rowsAffcted = command.ExecuteNonQuery();

                        if (rowsAffcted > 0)
                        {
                            Console.WriteLine("Produto deletado com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Produto não encontrado..");
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }

        }

        static string LerApenasLetras() // FUNÇÃO QUE VEREFICA SE FORAM INSERIDOS SOMENTE LETRAS NOS VALORES
        {
            string input = "";
            while (true)
            {
                string entrada = Console.ReadLine();
                if (ValidarApenasLetras(entrada))
                {
                    input = entrada;
                    break;
                }
                else
                {
                    Console.WriteLine("Erro, por favor, verificar e colocar somente letras.");
                }
            }
            return input;
        }

        static bool ValidarApenasLetras(string entrada)
        {
            foreach (char c in entrada)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                {
                    return false;
                }
            }
            return true;
        }

        static string LerApenasNumeros() // FUNÇÃO VERIFICADORA PARA VALORES QUE SOMENTE POSSA TER NUMEROS 
        {
            string input = "";
            while (true)
            {
                string entrada = Console.ReadLine();
                if (ValidarApenasNumeros(entrada))
                {
                    input = entrada;
                    break;
                }
                else
                {
                    Console.WriteLine("Erro, por favor, insira apenas Numeros.");
                }
            }
            return input;
        }

        static bool ValidarApenasNumeros(string entrada)
        {
            foreach (char c in entrada)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

    }


}
