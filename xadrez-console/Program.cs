using System;
using tabuleiro;
using xadrez;

namespace xadrez_console{

    class Program{

        public static void Main(string[] args){
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.imprimirTabuleiro(partida.tab);

                        Console.Write("Origem: ");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                        Console.Write("Destino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();

                        partida.executaMovimento(origem, destino);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                    
                }

                

                
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                
            }
            Console.ReadLine();
        }
    }
}
