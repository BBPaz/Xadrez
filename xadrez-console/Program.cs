using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace xadrez_console{

    class Program{

        public static void Main(string[] args){
            Posicao P = new Posicao(3,4);

            Console.WriteLine("Posição: "+P);
            Console.Read();
        }
    }
}
