using Xadrez.Entities.Tabuleiros;

namespace Xadrez.Entities.Pecas
{
    class Peca
    {
        public Posicao posicao { get; set; }
        public Cor CorP { get; protected set; }
        public int QuantidadeMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Cor cor, Tabuleiro tabuleiro) 
        {
            posicao = null;
            CorP = cor;
            Tab = tabuleiro;
            QuantidadeMovimentos = 0;
        }

        public void IncrementarMoves()
        {
            QuantidadeMovimentos++;
        }
    }
}
