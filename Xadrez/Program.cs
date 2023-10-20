using Xadrez;
using Xadrez.Entities.Tabuleiros;
using Xadrez.Entities.Pecas;
using Xadrez.Entities.Xadrez;

try
{
    PosicaoXadrez pos = new PosicaoXadrez('a',1);
    PartidaXadrez partida = new PartidaXadrez();
    while (!partida.terminada)
    {
        Console.Clear();
        Tela.ImprimirTabuleiro(partida.Tab);
        Console.WriteLine();
        Console.Write("Origem: ");
        Posicao origem = Tela.LerPosicaoXadrez().toPosicao();

        bool[,] posicoesP = partida.Tab.peca(origem).MovesP();

        Console.Clear();
        Tela.ImprimirTabuleiro(partida.Tab, posicoesP);

        Console.WriteLine();
        Console.Write("Destino: ");
        Posicao destino = Tela.LerPosicaoXadrez().toPosicao();
        Console.WriteLine(origem +" "+ destino);

        partida.ExecutaMovimento(origem, destino);
    }

}
catch(TabuleiroException e)
{
    Console.WriteLine(e.Message);
}

