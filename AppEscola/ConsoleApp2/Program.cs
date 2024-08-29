using MySql.Data.MySqlClient;
using System;

public class Program
{
    private static string connectionString = "Server=localhost;Database=escola;Uid=Daniel;Pwd=12345657;";

    static void Main(string[] args)
    {
        int opcao;
        do
        {
            Console.WriteLine("\nSistema de Controle de Alunos");
            Console.WriteLine("1 - Cadastrar Aluno");
            Console.WriteLine("2 - Listar Alunos");
            Console.WriteLine("3 - Atualizar Aluno");
            Console.WriteLine("4 - Excluir Aluno");
            Console.WriteLine("5 - Atualizar Data de Matrícula");
            Console.WriteLine("6 - Listar Alunos por Curso");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");
            opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    CadastrarAluno();
                    break;
                case 2:
                    ListarAlunos();
                    break;
                case 3:
                    AtualizarAluno();
                    break;
                case 4:
                    ExcluirAluno();
                    break;
                case 5:
                    AtualizarDataMatricula();
                    break;
                case 6:
                    ListarAlunosPorCurso();
                    break;
            }
        } while (opcao != 0);
    }

    static void CadastrarAluno()
    {
        Console.Write("Nome: ");
        string nome = Console.ReadLine();
        Console.Write("Idade: ");
        int idade = int.Parse(Console.ReadLine());
        Console.Write("Curso: ");
        string curso = Console.ReadLine();
        Console.Write("Data de Matrícula (yyyy-mm-dd): ");
        DateTime dataMatricula = DateTime.Parse(Console.ReadLine());

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO alunos (Nome, Idade, Curso, DataMatricula) VALUES (@nome, @idade, @curso, @dataMatricula)";
            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@idade", idade);
            cmd.Parameters.AddWithValue("@curso", curso);
            cmd.Parameters.AddWithValue("@dataMatricula", dataMatricula);

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        Console.WriteLine("Aluno cadastrado com sucesso!");
    }

    static void ListarAlunos()
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM alunos";
            var reader = cmd.ExecuteReader();

            Console.WriteLine("\nLista de Alunos:");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["Id"]}, Nome: {reader["Nome"]}, Idade: {reader["Idade"]}, Curso: {reader["Curso"]}, Data de Matrícula: {reader["DataMatricula"]}");
            }

            connection.Close();
        }
    }

    static void AtualizarAluno()
    {
        Console.Write("ID do Aluno: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Novo Nome: ");
        string nome = Console.ReadLine();
        Console.Write("Nova Idade: ");
        int idade = int.Parse(Console.ReadLine());
        Console.Write("Novo Curso: ");
        string curso = Console.ReadLine();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE alunos SET Nome = @nome, Idade = @idade, Curso = @curso WHERE Id = @id";
            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@idade", idade);
            cmd.Parameters.AddWithValue("@curso", curso);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        Console.WriteLine("Aluno atualizado com sucesso!");
    }

    static void ExcluirAluno()
    {
        Console.Write("ID do Aluno: ");
        int id = int.Parse(Console.ReadLine());

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM alunos WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        Console.WriteLine("Aluno excluído com sucesso!");
    }

    static void AtualizarDataMatricula()
    {
        Console.Write("ID do Aluno: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Nova Data de Matrícula (yyyy-mm-dd): ");
        DateTime dataMatricula = DateTime.Parse(Console.ReadLine());

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE alunos SET DataMatricula = @dataMatricula WHERE Id = @id";
            cmd.Parameters.AddWithValue("@dataMatricula", dataMatricula);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        Console.WriteLine("Data de matrícula atualizada com sucesso!");
    }

    static void ListarAlunosPorCurso()
    {
        Console.Write("Curso: ");
        string curso = Console.ReadLine();

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM alunos WHERE Curso = @curso";
            cmd.Parameters.AddWithValue("@curso", curso);
            var reader = cmd.ExecuteReader();

            Console.WriteLine($"\nAlunos matriculados no curso {curso}:");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["Id"]}, Nome: {reader["Nome"]}, Idade: {reader["Idade"]}, Data de Matrícula: {reader["DataMatricula"]}");
            }

            connection.Close();
        }
    }
}