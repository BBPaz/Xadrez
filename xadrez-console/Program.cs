using System;
using tabuleiro;
using xadrez;

namespace xadrez_console{

    class Program{

        public static void Main(string[] args){
            try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);
                tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
                tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
                tab.colocarPeca(new Rei(tab, Cor.Preta), new Posicao(10, 0));

                Tela.imprimirTabuleiro(tab);
                Console.ReadLine(); 
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
            
        }
    }
}
