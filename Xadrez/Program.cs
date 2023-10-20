using Xadrez;
using Xadrez.Entities.Tabuleiros;
using Xadrez.Entities.Pecas;

Tabuleiro tab = new Tabuleiro(8, 8);

tab.AddPeca(new Torre(Cor.Preta, tab), new Posicao(0, 0));
tab.AddPeca(new Torre(Cor.Preta, tab), new Posicao(1, 3));
tab.AddPeca(new Rei(Cor.Preta, tab), new Posicao(2, 4));

Tela.ImprimirTabuleiro(tab);
