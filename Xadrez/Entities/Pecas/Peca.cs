using Xadrez.Entities.Tabuleiros;

namespace Xadrez.Entities.Pecas
{
    abstract class Peca
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

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] mat = MovesP();
            for (int i = 0; i < Tab.Linhas; i++)
            {
                for (int j = 0; j < Tab.Colunas; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool podeMoverPara(Posicao posicao)
        {
            return MovesP()[posicao.Linha, posicao.Coluna];
        }
        public abstract bool[,] MovesP();
    }
}
