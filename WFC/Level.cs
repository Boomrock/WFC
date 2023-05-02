using System.Collections.Generic;
using System.Linq;

namespace WFC
{
    internal class Level<TypeOfContent>
    {
        //двойной лист потому что хранит в себе разные стороны
        private List<List<Neighbour<TypeOfContent>>> neighbours;

        public Level() {
            neighbours = new List<List<Neighbour<TypeOfContent>>>
            {
                new List<Neighbour<TypeOfContent>>(),
                new List<Neighbour<TypeOfContent>>(),
                new List<Neighbour<TypeOfContent>>(),
                new List<Neighbour<TypeOfContent>>()
            };
        }

        internal List<List<Neighbour<TypeOfContent>>> Neighbours { get => neighbours; set => neighbours = value; }

        public void AddNeighbour(Tile<TypeOfContent> Tile, Direction direction)
        {
            Neighbour<TypeOfContent> neighbour = new Neighbour<TypeOfContent>(Tile);
            if (neighbours[(int)direction].Contains(neighbour))
            {
                int index = neighbours[(int)direction].IndexOf(neighbour);
                neighbours[(int)direction][index].Count++;
                return;

            }

            neighbours[(int)direction].Add(neighbour);
        }


    }
}
