using System;
using Xadrez.Entities.Xadrez;
using Xadrez.Entities.Tabuleiros;
using Xadrez.Entities.Pecas;

namespace Xadrez.Entities.Xadrez
{
    class PartidaXadrez
    {
        public Tabuleiro Tab {  get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool terminada {  get; private set; }

        public PartidaXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            ColocarPecas();
            terminada = false;
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.removePeca(origem);
            p.IncrementarMoves();
            Peca pecaCapturada = Tab.removePeca(destino);
            Tab.AddPeca(p, destino);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            MudaJogador();
        }

        public void ValidarPosiOrigem(Posicao pos)
        {
            if(Tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if(JogadorAtual != Tab.peca(pos).CorP)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!Tab.peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há mmovimentos possiveis para a peça escolhida!");
            }

        }

        public void ValidarPosiDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino invalida!");
            }
        }

        private void MudaJogador()
        {
            if(JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        private void ColocarPecas()
        {
            Tab.AddPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('a',1).toPosicao());
            Tab.AddPeca(new Rei(Cor.Branca, Tab), new PosicaoXadrez('a', 2).toPosicao());
        }
    }
}
