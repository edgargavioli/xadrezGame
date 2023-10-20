using Xadrez.Entities.Tabuleiros;

namespace Xadrez.Entities.Pecas
{
    class PosicaoXadrez
    {
        public char Coluna {  get; set; }
        public int Linha { get; set; }

        public PosicaoXadrez(char coluna, int linha) 
        {
            Coluna = coluna;
            Linha = linha;
        }

        public Posicao toPosicao()
        {
            return new Posicao(8 - Linha,'a' - Coluna);
        }

        public override string ToString()
        {
            return ""+ Coluna + Linha;
        }
    }
}
