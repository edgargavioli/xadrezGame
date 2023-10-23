using Xadrez.Entities.Tabuleiros;
using Xadrez.Entities.Xadrez;

namespace Xadrez.Entities.Pecas
{
    class Rei : Peca
    {
        private PartidaXadrez Partida;
        public Rei(Cor cor, Tabuleiro tab, PartidaXadrez partida) : base(cor, tab)
        {
            Partida = partida;
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

        private bool TesteTorreRoque(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p != null && p is Torre && p.CorP == CorP && p.QuantidadeMovimentos == 0;
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

            //#Jogada especial Roque
            if(QuantidadeMovimentos == 0 && !Partida.xeque)
            { //#ROQUE PEQUENO
                Posicao pTorreP = new Posicao(posicao.Coluna + 3, posicao.Linha);
                if (TesteTorreRoque(pTorreP))
                {
                    Posicao p1 = new Posicao(posicao.Coluna + 1, posicao.Linha);
                    Posicao p2 = new Posicao(posicao.Coluna + 2, posicao.Linha);
                    if (Tab.peca(p1)==null & Tab.peca(p2)==null)
                    {
                        mat[posicao.Linha, posicao.Coluna + 2] = true;
                    }
                }
                //#ROQUE GRANDE
                Posicao pTorreG = new Posicao(posicao.Coluna - 4, posicao.Linha);
                if (TesteTorreRoque(pTorreG))
                {
                    Posicao p1 = new Posicao(posicao.Coluna - 1, posicao.Linha);
                    Posicao p2 = new Posicao(posicao.Coluna - 2, posicao.Linha);
                    Posicao p3 = new Posicao(posicao.Coluna - 3, posicao.Linha);
                    if (Tab.peca(p1) == null && Tab.peca(p2) == null && Tab.peca(p3) == null)
                    {
                        mat[posicao.Linha, posicao.Coluna - 2] = true;
                    }
                }
            }

            return mat;
        }

    }
}
