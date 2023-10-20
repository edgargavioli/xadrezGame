using System;

namespace Xadrez.Entities.Tabuleiros
{
    class TabuleiroException : Exception
    {
        public TabuleiroException(string msg) : base(msg){ }
    }
}
