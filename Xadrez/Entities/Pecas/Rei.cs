using Xadrez.Entities.Tabuleiros;

namespace Xadrez.Entities.Pecas
{
    class Rei : Peca
    {
        public Rei(Cor cor, Tabuleiro tab) : base(cor, tab)
        {

        }

        public override string ToString()
        {
            return "R";
        }

        private bool podeMover(Posicao pos)
        {
            Peca peca = Tab.peca(pos);
            return peca == null || peca.CorP != CorP;
        }

        public override bool[,] MovesP() 
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0,0);

            //Acima
            pos.definirValores(posicao.Linha - 1, posicao.Coluna);
            if(Tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //NE
            pos.definirValores(posicao.Linha - 1, posicao.Coluna+1);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Direita
            pos.definirValores(posicao.Linha, posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Sude
            pos.definirValores(posicao.Linha + 1, posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Abaixo
            pos.definirValores(posicao.Linha + 1, posicao.Coluna);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //sudo
            pos.definirValores(posicao.Linha + 1, posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //esq
            pos.definirValores(posicao.Linha, posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //
            pos.definirValores(posicao.Linha - 1, posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            return mat;
        }

    }
}
