using System;
using tabuleiro;
using xadrez;

namespace xadrez_console{

    class Program{

        public static void Main(string[] args){
            try
            {
                PosicaoXadrez pos = new PosicaoXadrez('a',1);

                Console.WriteLine(pos);

                Console.WriteLine(pos.toPosicao());

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
