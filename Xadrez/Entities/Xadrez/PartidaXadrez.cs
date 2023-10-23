using System.Collections.Generic;
using Xadrez.Entities.Xadrez;
using Xadrez.Entities.Tabuleiros;
using Xadrez.Entities.Pecas;

namespace Xadrez.Entities.Xadrez
{
    class PartidaXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; } = false;

        public PartidaXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            ColocarPecas();
            terminada = false;
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.removePeca(origem);
            p.IncrementarMoves();
            Peca pecaCapturada = Tab.removePeca(destino);
            Tab.AddPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        public  void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.removePeca(destino);
            p.DecrementarMoves();
            if(pecaCapturada != null)
            {
                Tab.AddPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            Tab.AddPeca(p, origem);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em cheque");
            }
            if (EstaEmXeque(Adversaria(JogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }

            Turno++;
            MudaJogador();
        }

        public void ValidarPosiOrigem(Posicao pos)
        {
            if (Tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (JogadorAtual != Tab.peca(pos).CorP)
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
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (var peca in capturadas)
            {
                if (peca.CorP == cor)
                {
                    aux.Add(peca);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecaInGame(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (var peca in pecas)
            {
                if (peca.CorP == cor)
                {
                    aux.Add(peca);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private Cor Adversaria(Cor cor)
        {
            if(cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca Rei(Cor cor)
        {
            foreach (var peca in pecaInGame(cor))
            {
                if(peca is Rei)
                {
                    return peca;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca rei = Rei(cor);
            if(rei == null)
            {
                throw new TabuleiroException("Não tem rei no tablueiro");
            }
            foreach(Peca x in pecaInGame(Adversaria(cor)))
            {
                bool[,] mat = x.MovesP();
                if (mat[rei.posicao.Linha, rei.posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.AddPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(Cor.Preta, Tab));
            colocarNovaPeca('a', 6, new Rei(Cor.Branca, Tab));
            colocarNovaPeca('b', 1, new Rei(Cor.Preta, Tab));
        }
    }
}
