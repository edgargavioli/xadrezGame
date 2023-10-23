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
        public Peca VulneravelEnPassant { get; private set; }
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
            VulneravelEnPassant = null;
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

            //#jogadaespecial ROQUE
           if(p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Coluna + 3, origem.Linha);
                Posicao destinoT = new Posicao(origem.Coluna + 1, origem.Linha);

                Peca t = Tab.removePeca(origemT);
                t.IncrementarMoves();
                Tab.AddPeca(t, destinoT);
            }
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Coluna - 4, origem.Linha);
                Posicao destinoT = new Posicao(origem.Coluna - 1, origem.Linha);

                Peca t = Tab.removePeca(origemT);
                t.IncrementarMoves();
                Tab.AddPeca(t, destinoT);
            }

            if(p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if(p.CorP == Cor.Branca)
                    {
                        posP = new Posicao(destino.Coluna, destino.Linha + 1);
                    }
                    else
                    {
                        posP = new Posicao(destino.Coluna, destino.Linha - 1);
                    }
                    pecaCapturada = Tab.removePeca(posP);
                    capturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.removePeca(destino);
            p.DecrementarMoves();
            if (pecaCapturada != null)
            {
                Tab.AddPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }

            Tab.AddPeca(p, origem);

            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);

                Peca t = Tab.removePeca(destinoT);
                t.DecrementarMoves();
                Tab.AddPeca(t, origemT);
            }
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);

                Peca t = Tab.removePeca(destinoT);
                t.DecrementarMoves();
                Tab.AddPeca(t, origemT);
            }
            //JOGADAESPECIAL
            if(p is Peao)
            {
                if(origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    Peca peao = Tab.removePeca(destino);
                    Posicao posP;
                    if(p.CorP == Cor.Branca)
                    {
                        posP = new Posicao(destino.Coluna, 3);
                    }
                    else
                    {
                        posP = new Posicao(destino.Coluna, 4);
                    }
                    Tab.AddPeca(peao, posP);
                }
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {

            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em cheque");
            }

            Peca p = Tab.peca(destino);
            //#JOGADAESPECIAL
            if(p is Peao)
            {
                if((p.CorP == Cor.Branca && destino.Linha == 0) || (p.CorP == Cor.Preta && destino.Linha == 7))
                {
                    p = Tab.removePeca(destino);
                    pecas.Remove(p);
                    Peca dama = new Rainha(p.CorP, Tab);
                    Tab.AddPeca(dama, destino);
                    pecas.Add(dama);
                }
            }

            if (EstaEmXeque(Adversaria(JogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }

            if (XequeMate(Adversaria(JogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }
           
            //#JOGADAESPECIAL

            if (p is Peao && destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2) 
            {
                VulneravelEnPassant = p;
            }
            else
            {
                VulneravelEnPassant = null;
            }
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
            if (cor == Cor.Branca)
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
                if (peca is Rei)
                {
                    return peca;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca rei = Rei(cor);
            if (rei == null)
            {
                throw new TabuleiroException("Não tem rei no tablueiro");
            }
            foreach (Peca x in pecaInGame(Adversaria(cor)))
            {
                bool[,] mat = x.MovesP();
                if (mat[rei.posicao.Linha, rei.posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool XequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in pecaInGame(cor))
            {
                bool[,] mat = x.MovesP();
                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.AddPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(Cor.Branca, Tab));
            colocarNovaPeca('b', 1, new Cavalo(Cor.Branca, Tab));
            colocarNovaPeca('c', 1, new Bispo(Cor.Branca, Tab));
            colocarNovaPeca('d', 1, new Rainha(Cor.Branca, Tab));
            colocarNovaPeca('e', 1, new Rei(Cor.Branca, Tab, this));
            colocarNovaPeca('f', 1, new Bispo(Cor.Branca, Tab));
            colocarNovaPeca('g', 1, new Cavalo(Cor.Branca, Tab));
            colocarNovaPeca('h', 1, new Torre(Cor.Branca, Tab));
            colocarNovaPeca('a', 2, new Peao(Cor.Branca, Tab, this));
            colocarNovaPeca('b', 2, new Peao(Cor.Branca, Tab, this));
            colocarNovaPeca('c', 2, new Peao(Cor.Branca, Tab, this));
            colocarNovaPeca('d', 2, new Peao(Cor.Branca, Tab, this));
            colocarNovaPeca('e', 2, new Peao(Cor.Branca, Tab, this));
            colocarNovaPeca('f', 2, new Peao(Cor.Branca, Tab, this));
            colocarNovaPeca('g', 2, new Peao(Cor.Branca, Tab, this));
            colocarNovaPeca('h', 2, new Peao(Cor.Branca, Tab, this));

            colocarNovaPeca('a', 8, new Torre(Cor.Preta, Tab));
            colocarNovaPeca('b', 8, new Cavalo(Cor.Preta, Tab));
            colocarNovaPeca('c', 8, new Bispo(Cor.Preta, Tab));
            colocarNovaPeca('d', 8, new Rainha(Cor.Preta, Tab));
            colocarNovaPeca('e', 8, new Rei(Cor.Preta, Tab, this));
            colocarNovaPeca('f', 8, new Bispo(Cor.Preta, Tab));
            colocarNovaPeca('g', 8, new Cavalo(Cor.Preta, Tab));
            colocarNovaPeca('h', 8, new Torre(Cor.Preta, Tab));
            colocarNovaPeca('a', 7, new Peao(Cor.Preta, Tab, this));
            colocarNovaPeca('b', 7, new Peao(Cor.Preta, Tab, this));
            colocarNovaPeca('c', 7, new Peao(Cor.Preta, Tab, this));
            colocarNovaPeca('d', 7, new Peao(Cor.Preta, Tab, this));
            colocarNovaPeca('e', 7, new Peao(Cor.Preta, Tab, this));
            colocarNovaPeca('f', 7, new Peao(Cor.Preta, Tab, this));
            colocarNovaPeca('g', 7, new Peao(Cor.Preta, Tab, this));
            colocarNovaPeca('h', 7, new Peao(Cor.Preta, Tab, this));
        }                        
    }
}
