using System;
using tabuleiro;

namespace xadrez
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tab, Cor cor) : base(tab, cor)
        {

        }

        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null;
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
