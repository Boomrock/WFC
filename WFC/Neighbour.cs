using System;
using System.Collections.Generic;
using System.Text;

namespace WFC
{
    internal class Neighbour<TypeOfContent> : Tile<TypeOfContent>
    {

        float frequency;
        // ToDo: 
        int count;
        public int Count { get => count; set => count = value; }
        public float Frequency
        {
            get => frequency;
            set
            {
                if (value <= 1 && value > 0)
                {
                    frequency = value;
                }

            }
        }

        public Neighbour(TypeOfContent content) : base(content)
        {
            count = 1;
        }

        public Neighbour(Tile<TypeOfContent> tile) : base(tile.Content)
        {
            count = 1; 
        }



    }
}
