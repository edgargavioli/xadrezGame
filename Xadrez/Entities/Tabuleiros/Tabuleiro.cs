using Xadrez.Entities.Pecas;

namespace Xadrez.Entities.Tabuleiros
{
    class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] Pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[Linhas, Colunas];
        }
        public Peca peca(int Linha, int Coluna)
        {
            return Pecas[Linha, Coluna];
        }
    }
}
