using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(tab.linhas - i+" ");
                for (int j = 0; j < tab.colunas; j++)
                {

                    imprimirPeca(tab.peca(i,j));
                    Console.Write(j == tab.colunas - 1 ? "\n" : "");
                }
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void imprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {

            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(tab.linhas - i + " ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    imprimirPeca(tab.peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                    Console.Write(j == tab.colunas - 1 ? "\n" : "");
                }
            }
            Console.WriteLine("  a b c d e f g h");
        }
        
        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1]+"");
            PosicaoXadrez pos = new PosicaoXadrez(coluna, linha);
            return pos;
        }

        public static void imprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {

                switch (peca.cor)
                {
                    case Cor.Branca:
                        Console.Write(peca + " ");
                        break;
                    case Cor.Preta:
                        ConsoleColor aux = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(peca + " ");
                        Console.ForegroundColor = aux;
                        break;
                    default:
                        break;
                }
                
            }

        }
        
    }
}
