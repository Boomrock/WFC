using System.Collections.Generic;
using System.Linq;

namespace WFC
{
    internal class Level<TypeOfContent>
    {

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
    enum Direction
    {
        Top,
        Left,
        Down,
        Right
    }
}
