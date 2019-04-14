using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DevOps
{
    public class Program
    {

        /*
         * A = 0
         * B = 1
         * C = 2
         * D = 3
         * E = 4
         * 
         * AA = 0   | AB = 185 | AC = 119 | AD = 152 | AE = 133
         * BA = 185 | BB = 0   | BC = 121 | BD = 150 | BE = 200
         * CA = 119 | CB = 121 | CC = 0   | CD = 174 | CE = 120
         * DA = 152 | DB = 150 | DC = 174 | DD = 0   | DE = 199
         * EA = 133 | EB = 200 | EC = 120 | ED = 199 | EE = 0
         * 
         */


        static void Main(string[] args)
        {
            List<Rota> caminho;
            int numCidades = 5;
            int custo = 0;
            Grafo grafo;

            Console.Write("JOÃO IRÁ PASSAR POR 5 CIDADES EM SUA VIAGEM. SEU GASTO DE UMA CIDADE A OUTRA PODE SER VISTA NOS DADOS ABAIXO ");
            CaixeiroGuloso.montaGrafo(out grafo, numCidades);
            caminho = new List<Rota>(numCidades);
            Console.WriteLine("\n");
            Console.Write("ESCOLHA UMA DAS CIDADES (A, B, C, D, E) PARA JOÃO INICIAR SUA JORNADA: ");
            int cidadeInicial = CaixeiroGuloso.retornaCidadeNum(Console.ReadLine().ToString());
            Console.WriteLine("\n");
            CaixeiroGuloso.encontraCaminho(grafo, caminho, cidadeInicial, ref custo);
            CaixeiroGuloso.imprimeCaminho(custo, caminho);
            Console.ReadKey();
        }

        public class Rota
        {
            public int cidade1, cidade2, custo;
        }

        public class Grafo
        {
            public int[,] M;
        }

        public static class CaixeiroGuloso
        {

            public static int distancia(int cidade1, int cidade2)
            {
                if ((cidade1 == 0 && cidade2 == 1) || (cidade1 == 1 && cidade2 == 0))
                    return 185;
                if ((cidade1 == 0 && cidade2 == 2) || (cidade1 == 2 && cidade2 == 0))
                    return 119;
                if ((cidade1 == 0 && cidade2 == 3) || (cidade1 == 3 && cidade2 == 0))
                    return 152;
                if ((cidade1 == 0 && cidade2 == 4) || (cidade1 == 4 && cidade2 == 0))
                    return 133;

                if ((cidade1 == 1 && cidade2 == 2) || (cidade1 == 2 && cidade2 == 1))
                    return 121;
                if ((cidade1 == 1 && cidade2 == 3) || (cidade1 == 3 && cidade2 == 1))
                    return 150;
                if ((cidade1 == 1 && cidade2 == 4) || (cidade1 == 4 && cidade2 == 1))
                    return 200;

                if ((cidade1 == 2 && cidade2 == 3) || (cidade1 == 3 && cidade2 == 2))
                    return 174;
                if ((cidade1 == 2 && cidade2 == 4) || (cidade1 == 4 && cidade2 == 2))
                    return 120;

                if ((cidade1 == 3 && cidade2 == 4) || (cidade1 == 4 && cidade2 == 3))
                    return 199;

                return 0;
            }

            public static string retornaCidade(int cidade)
            {
                switch (cidade)
                {
                    case 0:
                        return "A";
                    case 1:
                        return "B";
                    case 2:
                        return "C";
                    case 3:
                        return "D";
                    case 4:
                        return "E";
                    default:
                        return "R"; //erro
                }
            }

            public static int retornaCidadeNum(string cidade)
            {
                switch (cidade)
                {
                    case "A":
                        return 0;
                    case "B":
                        return 1;
                    case "C":
                        return 2;
                    case "D":
                        return 3;
                    case "E":
                        return 4;
                    default:
                        return 100; //erro
                }
            }

            public static void encontraCaminho(Grafo grafo, List<Rota> caminho, int cidadeInicial, ref int custo)
            {
                int i;
                int cid1 = cidadeInicial;
                int cid2 = 0;
                int menorCusto;
                int tamanho = caminho.Capacity;
                int[] pos = new int[grafo.M.Length];

                List<int> adicionados = new List<int>(grafo.M.Length);
                adicionados.Add(cidadeInicial);
                Rota rota = new Rota();
                custo = 0;

                for (i = 0; i < tamanho; i++)
                {
                    menorCusto = 0;
                    rota.cidade1 = cid1;

                    if (i == caminho.Capacity - 1)
                    {
                        menorCusto = grafo.M[cid1, cidadeInicial];
                        cid2 = cidadeInicial;
                    }
                    else
                    {
                        for (int j = 0; j < tamanho; j++)
                        {
                            if (j != cid1 && !adicionados.Contains(j))
                            {
                                if (grafo.M[cid1, j] != 0 && (grafo.M[cid1, j] < menorCusto || menorCusto == 0))
                                {
                                    menorCusto = grafo.M[cid1, j];
                                    cid2 = j;
                                }
                            }
                        }
                    }
                    rota.cidade2 = cid2;
                    rota.custo = menorCusto;
                    adicionados.Add(cid2);
                    custo += menorCusto;
                    caminho.Add(rota);
                    rota = new Rota();
                    cid1 = cid2;
                }
            }

            public static void imprimeCaminho(int custo, List<Rota> melhorRota)
            {
                Console.WriteLine("\n\nO O CUSTO PARA A VIAGEM SAINDO DA CIDADE ESCOLHIDA É: " + custo);
                Console.WriteLine("\n\nSENDO ESTE O MELHOR TRAJETO");
                Console.WriteLine("\n\n              DE               PARA             CUSTO ");
                foreach (Rota rota in melhorRota)
                    Console.Write("              " + retornaCidade(rota.cidade1) + "                  " + retornaCidade(rota.cidade2) + "                " + rota.custo + "\n");
                Console.WriteLine("\n");
            }

            public static void montaGrafo(out Grafo grafo, int numCidades)
            {
                grafo = new Grafo();
                grafo.M = new int[5, 5];

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (i < j)
                            grafo.M[i, j] = distancia(i, j);
                        else
                            if (i == j)
                            grafo.M[i, j] = distancia(i, j);
                        else
                            grafo.M[i, j] = distancia(i, j);
                    }
                }

                Console.WriteLine();
                Console.WriteLine();
                for (int i = 0; i < numCidades; i++)
                {
                    for (int j = 0; j < numCidades; j++)
                        Console.Write(retornaCidade(i) + retornaCidade(j) + " = " + grafo.M[i, j] + " | ");

                    Console.WriteLine();
                }
            }


        }
    }
}
