namespace tabuleiro
{
    public class Posicao
    {
        public int linha { get; set; }
        public int coluna { get; set; }

        public Posicao(int l, int c) {
            linha = l;
            coluna = c;
        }

        public void definirValores(int l, int c)
        {
            linha = l;
            coluna = c;
        }

        public override string ToString()
        {
            return linha + ", " + coluna;
        }
    }
}
