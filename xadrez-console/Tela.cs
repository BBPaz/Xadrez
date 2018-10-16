using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

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
                    if (tab.peca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        imprimirPeca(tab.peca(i, j));
                    }
                    
                    Console.Write(j==tab.colunas-1?"\n":"");
                }
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void imprimirPeca(Peca peca)
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
