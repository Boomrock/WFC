using System;
using System.Collections.Generic;
using System.Text;

namespace WFC
{
    internal class Neighbour<TypeOfContent> : Tile<TypeOfContent>
    {

        int count;
        public int Count { get => count; set => count = value; }


        public Neighbour(TypeOfContent content) : base(content)
        {
            count = 1;
        }

        public Neighbour(Tile<TypeOfContent> tile) : base(tile)
        {
            count = 1; 
        }


    }
}
