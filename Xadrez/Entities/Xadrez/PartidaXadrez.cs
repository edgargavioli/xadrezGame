using System;
using Xadrez.Entities.Xadrez;
using Xadrez.Entities.Tabuleiros;
using Xadrez.Entities.Pecas;

namespace Xadrez.Entities.Xadrez
{
    class PartidaXadrez
    {
        public Tabuleiro Tab {  get; private set; }
        private int Turno;
        private Cor JogadorAtual;
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

        private void ColocarPecas()
        {
            Tab.AddPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('a',1).toPosicao());
        }
    }
}
