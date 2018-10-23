using System;
using tabuleiro;

namespace xadrez
{
    class Peao : Peca
    {

        private PartidaDeXadrez partida;
        public Peao(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            this.partida = partida;
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null;
        }

        private bool existeInimigo(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null ? false : p.cor == cor ? false : true;
        }

        private bool podeCapturar(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return  p != null && p.cor != this.cor;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);
            if(this.cor == Cor.Branca)
            {
                //Captura nordeste
                pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
                if (tab.posicaoValida(pos) && podeCapturar(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                //Captura noroeste
                pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
                if (tab.posicaoValida(pos) && podeCapturar(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                switch (qteMovimentos)
                {
                    case 0:
                        //norte duplo
                        pos.definirValores(posicao.linha - 1, posicao.coluna);
                        if (tab.posicaoValida(pos) && podeMover(pos))
                        {
                            mat[pos.linha, pos.coluna] = true;
                            pos.definirValores(pos.linha - 1, pos.coluna);
                            if (tab.posicaoValida(pos) && podeMover(pos))
                            {
                                mat[pos.linha, pos.coluna] = true;
                            }
                        }
                        break;
                    default:
                        //norte
                        pos.definirValores(posicao.linha - 1, posicao.coluna);
                        if (tab.posicaoValida(pos) && podeMover(pos))
                        {
                            mat[pos.linha, pos.coluna] = true;

                        }

                        //en passant
                        if(posicao.linha == 3)
                        {
                            Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                            if (tab.posicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEnPassant)
                            {
                                mat[esquerda.linha-1, esquerda.coluna] = true;
                            }
                            Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                            if (tab.posicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEnPassant)
                            {
                                mat[direita.linha-1, direita.coluna] = true;
                            }
                        }

                        break;
                }
                
            }
            else
            {
                //Captura sudeste
                pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
                if (tab.posicaoValida(pos) && podeCapturar(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                //Captura sudoeste
                pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
                if (tab.posicaoValida(pos) && podeCapturar(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                switch (qteMovimentos)
                {
                    case 0:
                        //norte duplo
                        pos.definirValores(posicao.linha + 1, posicao.coluna);
                        if (tab.posicaoValida(pos) && podeMover(pos))
                        {
                            mat[pos.linha, pos.coluna] = true;
                            pos.definirValores(pos.linha + 1, pos.coluna);
                            if (tab.posicaoValida(pos) && podeMover(pos))
                            {
                                mat[pos.linha, pos.coluna] = true;
                            }
                        }
                        break;
                    default:
                        //sul
                        pos.definirValores(posicao.linha + 1, posicao.coluna);
                        if (tab.posicaoValida(pos) && podeMover(pos))
                        {
                            mat[pos.linha, pos.coluna] = true;

                        }

                        //en passant
                        if (posicao.linha == 4)
                        {
                            Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                            if (tab.posicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEnPassant)
                            {
                                mat[esquerda.linha+1, esquerda.coluna] = true;
                            }
                            Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                            if (tab.posicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEnPassant)
                            {
                                mat[direita.linha+1, direita.coluna] = true;
                            }
                        }

                        break;
                }


            }


            return mat;

        }

        public override string ToString()
        {
            return "P";
        }
    }
}
