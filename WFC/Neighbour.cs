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

        public Neighbour(Tile<TypeOfContent> tile) : base(tile)
        {
            count = 1; 
        }
        public static void CalculateFrequency(List<Neighbour<TypeOfContent> >neighbours)
        {
            int TotalCount = 0;
            foreach (var neighbour in neighbours)
                TotalCount += neighbour.Count;
            foreach (var neighbour in neighbours)
                neighbour.Frequency = (float)neighbour.Count / TotalCount;
        }

    }
}
