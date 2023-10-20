using Xadrez.Entities.Tabuleiros;

namespace Xadrez.Entities.Pecas
{
    class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor CorP { get; protected set; }
        public int QuantidadeMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Posicao posicao, Cor cor, Tabuleiro tabuleiro) 
        {
            Posicao = posicao;
            CorP = cor;
            Tab = tabuleiro;
            QuantidadeMovimentos = 0;
        }
    }
}
