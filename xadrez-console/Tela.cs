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
                for(int j = 0; j < tab.colunas; j++)
                {
                    Console.Write(tab.peca(i, j) == null ?" -":" "+tab.peca(i,j).ToString());
                    Console.Write(j==tab.colunas-1?"\n":"");
                }
            }
        }
        
    }
}
