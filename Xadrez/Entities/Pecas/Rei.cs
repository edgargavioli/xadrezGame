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
    }
}
