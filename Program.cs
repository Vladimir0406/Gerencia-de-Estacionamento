using System;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
class Veiculo
{
    public string placa;
    public string Marca;
}
class Elemento
{
    public Elemento Prox;
    public Veiculo Carro;
    public Elemento()
    {
        Prox = null;
    }
}
class Fila
{
    public Elemento Inicio;
    public Elemento Fim;
    public Elemento Aux;
    public Elemento Novo;
    public int Tamanho;

    public Fila()
    {
        Inicio = null;
        Fim = null;
        Aux = null;
        Tamanho = 0;
    }

    public void Inserir(Veiculo X)
    {
        Tamanho++;

        Novo = new Elemento();
        Novo.Carro = X;

        if (Inicio == null)
        {
            Inicio = Novo;
            Fim = Novo;
        }
        else
        {
            Fim.Prox = Novo;
            Fim = Novo;
        }
    }
    public void Remover()//Remove o primeiro da fila
    {
        if (Tamanho > 0)
        {
            string X = Inicio.Carro.placa;

            if (Tamanho == 1)
                Inicio = null;

            else
                Inicio = Inicio.Prox;

            Console.WriteLine($"O veiculo de placa {X} foi removido do estacionamento.");

            Tamanho--;
        }
        else
            Console.WriteLine("Não há veiculos estacionados");
    }
    public void Remover(int Posiçao) // Remove baseado em posiçao
    {
        if (Tamanho > 0)
        {
            if (Posiçao <= Tamanho)
            {
                int i = 1;

                // INEFICIENTE(PERCORRE SO ATE O VALOR, MElhor + até o final)
                while (i < Posiçao)
                {
                    Tamanho--;
                    Inserir(Inicio.Carro);
                    Inicio = Inicio.Prox;
                    i++;
                }

                if (i == Posiçao)
                {
                    Console.WriteLine($"Veiculo de placa {Inicio.Carro.placa} removido");
                    Inicio = Inicio.Prox;
                    Tamanho--;
                }
            }
            else
            {
                Console.WriteLine("posição invalida.");
            }
        }
        else
            Console.WriteLine("Não há veiculos");
    }
    public void Remover(string Placa) // Remove baseado em placa 
    {
        if (Tamanho > 0)
        {
            bool Existe = false;

            Aux = Inicio;

            while (Aux != null)
            {
                if (Aux.Carro.placa == Placa)
                    Existe = true;

                Aux = Aux.Prox;
            }

            if (Existe)
            {
                Elemento X = new Elemento();

                while (Inicio.Carro.placa != Placa)
                {
                    Tamanho--;
                    Inserir(Inicio.Carro);
                    Inicio = Inicio.Prox;
                }

                if (Inicio.Carro.placa == Placa)
                {
                    Inicio = Inicio.Prox;
                    Tamanho--;
                }

                Console.WriteLine($"\nVeiculo de placa {Placa} removido.\n");
            }
            else
                Console.WriteLine($"\nA placa {Placa} não existe.\n");
        }
        else
            Console.WriteLine("Não há veiculos\n");
    }
    public void Limpar()
    {
        if (Tamanho == 0)
            Console.WriteLine("Não há veiculos no estacionamento.");
        else
        {
            Tamanho = 0;
            Inicio = null;
            Console.WriteLine("Veiculos removidos.");
        }
    }
    public void Listar()
    {
        if (Tamanho > 0)
        {
            Aux = Inicio;
            int i = 1;

            Console.WriteLine("");
            while (Aux != null)
            {
                Console.WriteLine($"Veiculo {{{i}}}");
                Console.WriteLine($"Placa : {Aux.Carro.placa} - Modelo : {Aux.Carro.Marca}\n");
                Aux = Aux.Prox;
                i++;
            }
        }
        else
        {
            Console.WriteLine("Não há veiculos estacionados.");
        }
    }
}
class Program
{
    static Veiculo CriaVeiculo()
    {
        Veiculo Carro = new Veiculo();

        Console.WriteLine("\nDados do veiculo");
        Console.WriteLine("----------------");
        Console.Write("Placa do carro : ");
        Carro.placa = Console.ReadLine();
        Console.Write("Modelo do carro : ");
        Carro.Marca = Console.ReadLine();

        return Carro;
    }
    static void Main(string[] args)
    {
        Fila Estacionamento = new Fila();
        string opçao;

        Console.Clear();

        do
        {
            int Vagas = 5 - Estacionamento.Tamanho;

            Console.Clear();
            Console.WriteLine("\n Sitema de controle de estacionamento");
            Console.WriteLine("======================================");
            Console.WriteLine(" 1 - Adicionar veiculo");
            Console.WriteLine(" 2 - Remover veiculo");
            Console.WriteLine(" 3 - Listar veiculos");
            Console.WriteLine(" 4 - Remover todos veiculos");
            Console.WriteLine(" 5 - Sair\n");

            opçao = Console.ReadLine();

            switch (opçao)
            {
                case "1":
                    if (Estacionamento.Tamanho < 5)
                    {
                        Console.Clear();
                        Estacionamento.Inserir(CriaVeiculo());
                        Console.WriteLine($"{{{5 - Estacionamento.Tamanho}}} vaga(s) restante(s).\n");
                    }
                    else
                        Console.WriteLine("\nNão há vagas");

                    Console.ReadKey();

                    break;
                case "2":

                    if (Vagas == 5)
                    {
                        Console.WriteLine("Não há veiculos estacionados.");
                        Console.ReadKey();

                        break;
                    }

                    Console.Clear();

                    Console.WriteLine("\nRemoção de veiculos");
                    Console.WriteLine("-------------------\n");
                    Console.WriteLine("1 - Remover o primeiro da fila.");
                    Console.WriteLine("2 - Remover pela placa.");
                    Console.WriteLine("3 - Remover pela posição.(1-5)\n");
                    int escolha = int.Parse(Console.ReadLine());

                    if (escolha == 1)// Remove o primeiro da fila
                        Estacionamento.Remover();

                    else if (escolha == 2)// Remove pela placa 
                    {
                        Console.WriteLine("Informe a placa do veiculo.");
                        Estacionamento.Remover(Console.ReadLine());
                    }
                    else if (escolha == 3)// Remove o veiculo na posição x
                    {
                        Estacionamento.Listar();

                        Console.WriteLine("Informe a posição do veiculo");
                        Estacionamento.Remover(int.Parse(Console.ReadLine()));
                        Console.WriteLine("");
                    }
                    else
                        Console.WriteLine("opçao invalida");

                    Console.ReadLine();

                    break;
                case "3":
                    if (Vagas == 5)
                    {
                        Console.WriteLine("Não há veiculos estacionados.");
                        Console.ReadKey();

                        break;
                    }

                    Console.Clear();
                    Estacionamento.Listar();
                    if (Vagas == 1)
                        Console.WriteLine($"{{{5 - Estacionamento.Tamanho}}} vaga diponivel.\n");

                    else
                        Console.WriteLine($"{{{5 - Estacionamento.Tamanho}}} vagas diponiveis.\n");

                    Console.ReadKey();

                    break;
                case "4":

                    Estacionamento.Limpar();
                    Console.ReadKey();

                    break;
                case "5":

                    Console.WriteLine("Encerrando....");
                    Thread.Sleep(500);

                    break;
                default:

                    Console.WriteLine("Opção invalida.");
                    Console.ReadKey();

                    break;
            }
        } while (opçao != "5");
    }
}
