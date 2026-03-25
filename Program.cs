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
            Menu();
        }
        // Métodos para formatação do programa
        static void Cabecalho(string title)
        {
            int larguraTotal = 70;
            string linhaPadrao = new string('-', larguraTotal);
            string linhaDecorada = new string('*', larguraTotal);

            //Centralização do titulo

            int espacosEsquerda = (larguraTotal + title.Length)/ 2;
            Console.WriteLine(linhaPadrao);
            Console.WriteLine(title.PadLeft(espacosEsquerda).PadRight(larguraTotal));
            Console.WriteLine(linhaPadrao);
            
           
        }
        static void Rodape()
        {
            int larguraTotal = 70;
            Console.WriteLine(new String('-', larguraTotal));
            Console.WriteLine(new String('*', larguraTotal));

        }
        static void Linha()
        {
            int larguraTotal = 70;
            Console.WriteLine(new String('-', larguraTotal));
        }
        static void LinhaDecorada()
        {
            int larguraTotal = 70;
            Console.WriteLine(new String('*', larguraTotal));
        }
        static void Carregando()
        {
            Console.WriteLine("Carregando...");
            Thread.Sleep(3000);
        }
        // Funcionalidades do programa
        static void Menu()
        {
            Console.Clear();
            Cabecalho("MENU");
            Console.WriteLine("[1]- Abrir arquivo");
            Console.WriteLine("[2]- Criar arquivo");
            Console.WriteLine("[0]- Sair ");
            Thread.Sleep(1000);
            Console.WriteLine("  Observação !!!!  ");
            Thread.Sleep(1000);
            Console.WriteLine("Para editar ou deletar seu arquivo");
            Thread.Sleep(1000);
            Console.WriteLine("Acesse [1] - Abrir arquivo");
            Rodape();
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
            Cabecalho("FOLDER");
            Console.WriteLine("Qual o caminho arquivo você deseja abrir? ");
            string path = Console.ReadLine();

            Rodape();
            Carregando();
            if(File.Exists(path)) {
                Console.WriteLine("");
                Linha();
                Console.WriteLine("Texto original");
                Linha();
            string text = File.ReadAllText(path);

                using (var file = new StreamReader(path))
            {
                
                text = file.ReadToEnd();
                Console.WriteLine(text);
            }
                
                ValidarProximoPasso(path, text);

        }else{
            Console.WriteLine("Arquivo não encontrado!!! Por favor, tente novamente.");
            Rodape();
            Thread.Sleep(2000);
            Abrir();
        }
        }

        static void ValidarProximoPasso(string path, string text)
        {
            
            var arquivo = path;
            var conteudo = text;
            Cabecalho(path);
            Console.WriteLine(" O que você deseja fazer com o seu arquivo?");
            Console.WriteLine("[1]- Editar");
            Console.WriteLine("[2]- Deletar");
            Console.WriteLine("[3]- Menu incial ");
            Console.WriteLine("[0]- Finalizar ");
            Rodape();
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
            Cabecalho("NEW FILE");
            LinhaDecorada();
            Console.WriteLine("  Digite seu texto abaixo:  | [OK] - Encerrar ");
            LinhaDecorada();
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

            Linha();
            Console.WriteLine("  Texto concluido  ");
            Console.Write(text);
            Rodape();
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
                AlgoMais();
            }

        }
        static void Editar(string arquivo, string conteudo)
        {
            Console.Clear();
            Cabecalho(arquivo);
            Console.WriteLine(conteudo);
            Thread.Sleep(2000);
            Linha();
            Console.WriteLine("Continue digitando o seu texto: ");
            Carregando();
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

            Console.WriteLine("  Texto atualizado  ");
            Rodape();
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
                Menu();
            }

        }
        static void Salvar(string text)
        {
            
            Console.Clear();
            Cabecalho("SAVE");
            Console.WriteLine(" Digite o caminho que será salvo o documento ");
            var path = Console.ReadLine();
            Carregando();
            if (File.Exists(path))
            {
                using (var file = new StreamWriter(path))
                {
                    file.Write(text);
                }

                Console.WriteLine($"Arquivo {path} foi atualizado com sucesso");
            }
            else
            {
                using (var file = new StreamWriter(path))
                {
                    file.Write(text);
                }

                Console.WriteLine($"Arquivo {path} foi criado com sucesso");
                
            }

            Rodape();
            AlgoMais();

        }

        static void AlgoMais()
        {
            Console.Clear();
            Cabecalho("Algo mais?");
            Console.WriteLine("Escolha uma opção: ");
            Console.WriteLine("[1] - Menu  || [2] - Finalizar ");
            Linha();
            short opcao = short.Parse(Console.ReadLine());
            if(opcao == 1)
            {
                Menu();
            }
            else
            {
                Finalizar();
            }

        }

        static void Deletar(string arquivo)
        {
            Console.Clear();
            Cabecalho("Recycle Bin");
            Console.WriteLine($"Tem certeza que deseja excluir o arquivo {arquivo}?");
            Console.WriteLine("[1] - Sim | [2] - Não");
            short confirmacao = short.Parse(Console.ReadLine());
            if (confirmacao == 1)
            {
                Linha();
                File.Delete(arquivo); //  Metodo deleta o arquivo usando o método File.Delete
                Carregando();
                Console.WriteLine($"Arquivo {arquivo} deletado com sucesso");
                Rodape();
                Thread.Sleep(2000);
                AlgoMais();

            }
            else
            {
                Console.WriteLine("Operação cancelada com sucesso!");
                Rodape();
                AlgoMais();
            }

        }
        static void Finalizar()
        {
            Console.Clear();
            Cabecalho("EXIT");
            Console.WriteLine("  VOLTE SEMPRE!! ");
            Rodape();
            System.Environment.Exit(0);
        }

    }
}