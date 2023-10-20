﻿using Xadrez.Entities.Tabuleiros;

namespace Xadrez.Entities.Pecas
{
    class Torre : Peca
    {
        public Torre(Cor cor, Tabuleiro tab) : base(cor, tab)
        {

        }

        private bool podeMover(Posicao pos)
        {
            Peca peca = Tab.peca(pos);
            return peca == null || peca.CorP != CorP;
        }

        public override bool[,] MovesP()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0, 0);

            //Acima
            pos.definirValores(posicao.Linha - 1, posicao.Coluna);
            while(Tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos)!= null && Tab.peca(pos).CorP != CorP)
                {
                    break;
                }
                pos.Linha = pos.Linha - 1;
            }
            //Abaixo
            pos.definirValores(posicao.Linha + 1, posicao.Coluna);
            while (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).CorP != CorP)
                {
                    break;
                }
                pos.Linha = pos.Linha + 1;
            }
            //Direita
            pos.definirValores(posicao.Linha, posicao.Coluna+1);
            while (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).CorP != CorP)
                {
                    break;
                }
                pos.Coluna = pos.Coluna + 1;
            }
            pos.definirValores(posicao.Linha, posicao.Coluna - 1);
            while (Tab.PosicaoValida(pos) && podeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.peca(pos) != null && Tab.peca(pos).CorP != CorP)
                {
                    break;
                }
                pos.Coluna = pos.Coluna - 1;
            }
       
            return mat;
        }

        public override string ToString()
        {
            return "T";
        }
    }
}
