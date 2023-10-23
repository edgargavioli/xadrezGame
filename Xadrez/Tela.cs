using System.Collections.Generic;
using Xadrez.Entities.Pecas;
using Xadrez.Entities.Tabuleiros;
using Xadrez.Entities.Xadrez;

namespace Xadrez
{
    class Tela
    {
        public static void ImprimirPartida(PartidaXadrez partida)
        {
            ImprimirTabuleiro(partida.Tab);
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine("Turno: " + partida.Turno);
            if (!partida.terminada)
            {
                Console.WriteLine("Aguardando da jogada: " + partida.JogadorAtual);
                if (partida.xeque)
                {
                    Console.WriteLine("XEQUE!!");
                }
            }
            else
            {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine("Vencedor: "+partida.JogadorAtual);
            }
        }

        public static void ImprimirPecasCapturadas(PartidaXadrez partida)
        {
            Console.WriteLine("Peças Capturadas: ");
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.pecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = aux;
            ImprimirConjunto(partida.pecasCapturadas(Cor.Preta));
        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca p in conjunto)
            {
                Console.Write(p+",");
            }
            Console.WriteLine("]");
        }
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    ImprimirPeca(tab.peca(i, j));
                }
                Console.WriteLine("");
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] possisoesP)
        {
            ConsoleColor fundoO = Console.BackgroundColor;
            ConsoleColor fundoA = ConsoleColor.DarkGray;
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (possisoesP[i, j])
                    {
                        Console.BackgroundColor = fundoA;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoO;
                    }
                    ImprimirPeca(tab.peca(i, j));
                    Console.BackgroundColor = fundoO;
                }
                Console.WriteLine("");
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoO;
        }

        public static void ImprimirPeca(Peca peca)
        {
            if(peca == null)
            {
                 Console.Write("- ");
            }
            else
            {
                if (peca.CorP == Cor.Branca)
                {
                    Console.Write(peca + " ");
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca + " ");
                    Console.ForegroundColor = aux;
                }
            }
            
        }
        
        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = char.Parse(s[0] + "");
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }
    }
}
