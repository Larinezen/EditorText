using System;
using System.IO;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace TextEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-----------------------------");
            Console.WriteLine("----------CADERNO------------");
            Console.WriteLine("-----------------------------");
            Menu();
        }

        static void Menu()
        {
            Console.WriteLine("Menu: ");
            Console.WriteLine("Para editar ou deletar seu arquivo, acesse [1] ");
            Console.WriteLine("[1]- Abrir arquivo");
            Console.WriteLine("[2]- Criar arquivo");
            Console.WriteLine("[0]- Sair ");
            short option = short.Parse(Console.ReadLine());
            switch (option)
            {
                case 0: Finalizar(); break;
                case 1: Abrir(); break;
                case 2: Criar(); break;
                default: Menu(); break;
            }
        }

        static void Abrir()
        {
            Console.Clear();
            Console.WriteLine("Qual o caminho arquivo você deseja abrir? ");
            string path = Console.ReadLine();
            string text = File.ReadAllText(path);

            using (var file = new StreamReader(path))
            {
                text = file.ReadToEnd();
                Console.WriteLine(text);
            }
            ValidarProximoPasso(path, text);

        }

        static void ValidarProximoPasso(string path, string text)
        {
            var arquivo = path;
            var conteudo = text;
            Console.WriteLine(" O que você deseja fazer com o seu arquivo?");
            Console.WriteLine("[1]- Editar");
            Console.WriteLine("[2]- Deletar");
            Console.WriteLine("[3]- Menu incial ");
            Console.WriteLine("[0]- Finalizar ");
            short opt = short.Parse(Console.ReadLine());
            switch (opt)
            {
                case 0: Finalizar(); break;
                case 1: Editar(arquivo, conteudo); break;
                case 2: Deletar(arquivo); break;
                case 3: Menu(); break;
                default: Menu(); break;

            }
        }
        static void Criar()
        {
            Console.Clear();
            Console.WriteLine("***********************************************");
            Console.WriteLine("  Digite seu texto abaixo:  | [OK] - Encerrar ");
            Console.WriteLine("***********************************************");
            string text = "";
            string linha;
            do
            {
                linha = Console.ReadLine();

                if (linha.ToUpper() != "OK")
                {
                    text += linha + Environment.NewLine;
                }
            } while (linha.ToUpper() != "OK");

            Console.WriteLine(" ------ Texto concluido ------");
            Console.Write(text);
            Thread.Sleep(2000);
            Console.WriteLine("Deseja salvar seu arquivo de texto? ");
            Console.WriteLine("[S] - Sim | [N] - Não ");

            string opcaoDigitada = Console.ReadLine().ToUpper();

            char opc = char.Parse(opcaoDigitada);

            if (opc == 'S')
            {
                Salvar(text);
            }
            else
            {
                Finalizar();
            }

        }
        static void Editar(string arquivo, string conteudo)
        {
            Console.WriteLine(conteudo);
            Thread.Sleep(2000);
            Console.WriteLine("Continue digitando o seu texto: ");
            string newText = ""; // Variável para armazenar o novo texto editado junto com o antigo
            string newLines;

            do
            {
                newLines = Console.ReadLine();  // Lê a nova linha de texto

                if (newLines.ToUpper() != "OK") // Verifica se a linha digitada é "OK" (ignora maiúsculas/minúsculas)
                {
                    conteudo += newLines + Environment.NewLine; // Adiciona a nova linha ao texto existente, seguido de uma quebra de linha
                }
            } while (newLines.ToUpper() != "OK");

            newText = conteudo; // Atualiza o texto com as novas linhas adicionadas

            Console.WriteLine(" ------ Texto atualizado ------");
            Thread.Sleep(2000);
            Console.WriteLine(newText);
            Thread.Sleep(2000);
            Console.WriteLine("Deseja salvar as alterações do seu arquivo de texto? ");
            Console.WriteLine("[S] - Sim | [N] - Não ");
            string opcSave = Console.ReadLine().ToUpper();

            if (opcSave == "S")
            {
                Salvar(newText);

            }
            else
            {
                Console.Clear();
                Menu();
            }

        }
        static void Salvar(string text)
        {
            Thread.Sleep(2000);
            Console.Clear();
            Console.WriteLine(" Digite o caminho que será salvo o documento ");
            var path = Console.ReadLine();

            using (var file = new StreamWriter(path))
            {
                file.Write(text);
            }

            Console.WriteLine($"Arquivo {path} salvo com sucesso");
            Menu();


        }

        static void Deletar(string arquivo)
        {
            Console.WriteLine($"Tem certeza que deseja excluir o arquivo {arquivo}?");
            Console.WriteLine("[1] - Sim | [2] - Não");
            short confirmacao = short.Parse(Console.ReadLine());
            if (confirmacao == 1)
            {
                File.Delete(arquivo); //  Metodo deleta o arquivo usando o método File.Delete
                Console.WriteLine($"Arquivo {arquivo} deletado com sucesso");
                Thread.Sleep(2000);
                Menu();

            }
            else
            {
                Console.WriteLine("Operação cancelada com sucesso!");
                Console.Clear();
                Menu();
            }

        }
        static void Finalizar()
        {
            Console.WriteLine(" *** VOLTE SEMPRE *** ");
            System.Environment.Exit(0);
        }

    }
}