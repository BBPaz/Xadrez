using System.Collections.Generic;
using tabuleiro;


namespace xadrez
{
    public class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }
        public Peca vulneravelEnPassant { get; private set; }
        private bool passant;

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            passant = false;
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            if(pecaCapturada!=null)
            capturadas.Add(pecaCapturada);
            tab.colocarPeca(p, destino);
            
            if(p is Rei)
            {
                //roque pequeno
                if (destino.coluna == origem.coluna + 2)
                {
                    Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                    Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                    Peca T = tab.retirarPeca(origemT);
                    T.incrementarQteMovimentos();
                    tab.colocarPeca(T, destinoT); 
                }
                //roque grande
                if (destino.coluna == origem.coluna - 2)
                {
                    Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                    Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                    Peca T = tab.retirarPeca(origemT);
                    T.incrementarQteMovimentos();
                    tab.colocarPeca(T, destinoT);
                }
            }

            //en passant
            if(p is Peao && destino.coluna != origem.coluna && pecaCapturada==null)
            {
                if (p.cor == Cor.Branca)
                {
                    p = tab.retirarPeca(destino);
                    tab.colocarPeca(p, new Posicao(destino.linha, destino.coluna));
                    pecaCapturada = tab.retirarPeca(new Posicao(destino.linha + 1, destino.coluna));
                    capturadas.Add(pecaCapturada);
                }
                else
                {
                    p = tab.retirarPeca(destino);
                    tab.colocarPeca(p, new Posicao(destino.linha, destino.coluna));
                    pecaCapturada = tab.retirarPeca(new Posicao(destino.linha - 1, destino.coluna));
                    capturadas.Add(pecaCapturada);
                }
                passant = true;
            }

            return pecaCapturada;

        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca pMov = tab.retirarPeca(destino);
            pMov.reduzirQteMovimentos();
            if (pecaCapturada != null)
            {
                capturadas.Remove(pecaCapturada);
                tab.colocarPeca(pecaCapturada, destino);
            }
            tab.colocarPeca(pMov, origem);

            //en passant
            int colPassant = origem.coluna - destino.coluna;
            if (pMov is Peao && colPassant!=0 && pecaCapturada == vulneravelEnPassant && passant == true)
            {
                Peca peaoEnPassant = tab.retirarPeca(destino);
                tab.colocarPeca(peaoEnPassant, new Posicao(origem.linha, destino.coluna));
                passant = false;
            }

            //roque pequeno
            if (pMov is Rei)
            {
                if (destino.coluna == origem.coluna + 2)
                {
                    Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                    Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                    Peca T = tab.retirarPeca(destinoT);
                    T.reduzirQteMovimentos();
                    tab.colocarPeca(T, origemT);
                }
                //roque grande
                if (destino.coluna == origem.coluna - 2)
                {
                    Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                    Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                    Peca T = tab.retirarPeca(destinoT);
                    T.reduzirQteMovimentos();
                    tab.colocarPeca(T, destinoT);
                }
            }
            

        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executaMovimento(origem, destino);
            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode colocar seu rei em xeque.");
            }
            if (estaEmXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }
            if (testeXequeMate(adversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                turno++;
                mudaJogador();
            }

            Peca p = tab.peca(destino);
            //#jogadaespecial en passant
            if (p is Peao && destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2)
            {
                vulneravelEnPassant = p;
            }
            else
            {
                vulneravelEnPassant = null;
            }
        }

        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Preta)
            {
                return Cor.Branca;
            }
            else
            {
                return Cor.Preta;
            }
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if(x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool testeXequeMate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach(Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for(int i = 0; i < tab.linhas; i++)
                {
                    for(int j = 0;j<tab.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = new Posicao(x.posicao.linha,x.posicao.coluna);
                            Posicao destino = new Posicao(i,j);
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }

                        }
                    }
                }
            }
            return true;
        }

        public bool estaEmXeque(Cor cor)
        {
            Peca r = rei(cor);
            if (r == null)
            {
                throw new TabuleiroException("Não há rei da cor " + cor + " no tabuleiro");
            }
            //foreach(Peca x in pecasEmJogo(adversaria(cor)))
            HashSet<Peca> pecasadv = pecasEmJogo(adversaria(cor));
            foreach (Peca x in pecasadv)
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[r.posicao.linha, r.posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool estaEmXeque(Cor cor, Posicao pos)
        {
            HashSet<Peca> pecasadv = pecasEmJogo(adversaria(cor));
            foreach (Peca x in pecasadv)
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[pos.linha, pos.coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public void validarPosicaoOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida.");
            }
            if (jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua.");
            }
            if (!tab.peca(pos).existeMovimentoPossivel())
            {
                throw new TabuleiroException("O caminho da peça escolhida está completamente bloqueado.");
            }
        }

        public void validarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).movimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino inválida");
            }
        }

        private void mudaJogador()
        {
            if(jogadorAtual == Cor.Preta)
            {
                jogadorAtual = Cor.Branca;
            }
            else
            {
                jogadorAtual = Cor.Preta;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capturadas)
            {
                if(x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca,new PosicaoXadrez(coluna,linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas()
        {
            /*
            //Testes
            colocarNovaPeca('b', 4, new Peao(tab, Cor.Branca, this));
            tab.peca(new PosicaoXadrez('b',4).toPosicao()).incrementarQteMovimentos();
            colocarNovaPeca('b', 1, new Rei(tab, Cor.Branca, this));
            colocarNovaPeca('c', 5, new Peao(tab, Cor.Preta, this));
            tab.peca(new PosicaoXadrez('c', 5).toPosicao()).incrementarQteMovimentos();
            vulneravelEnPassant = tab.peca(new PosicaoXadrez('c', 5).toPosicao());
            colocarNovaPeca('b', 8, new Torre(tab, Cor.Preta));
            //colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
            //fim testes
            */

            ///*
            colocarNovaPeca('a',2,new Peao(tab,Cor.Branca, this));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca, this));


            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));

            colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta, this));

            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta,this));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            //*/



            /*colocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));

            colocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));*/

        }
    }
}
