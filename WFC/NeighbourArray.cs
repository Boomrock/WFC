using System;
using System.Collections.Generic;
using System.Text;

namespace WFC
{
    internal class NeighbourArray<TypeOfContent>
    {
        private TileList<Neighbour<TypeOfContent>,TypeOfContent>[,] neighbours;
        private int[] center = new int[2];

        public NeighbourArray(int CountLayers)
        {
            /*
            #####
            #####
            ## ##
            #####
            #####
           #######
             */
            neighbours = new TileList<Neighbour<TypeOfContent>, TypeOfContent>[CountLayers * 2 + 1, CountLayers * 2 + 1];
            center[0] = neighbours.GetLength(0) / 2; 
            center[1] = neighbours.GetLength(1) / 2; 
        }

        internal TileList<Neighbour<TypeOfContent>, TypeOfContent>this[int y, int x]
        {
            get
            { 

                return neighbours[center[0] + y, center[1] + x];
            }
            set {

                neighbours[center[0] + y, center[1] + x] = value;
            }
        }

        internal int GetLength(int dimension)
        {
            return neighbours.GetLength(dimension);
        }

        internal void AddNeighbour(Tile<TypeOfContent> tile, int offsetY, int offsetX)
        {
            if(this[offsetY, offsetX] == null)
                this[offsetY, offsetX] = new TileList<Neighbour<TypeOfContent>, TypeOfContent>();
            TileList<Neighbour<TypeOfContent>, TypeOfContent> neighbour = this[offsetY, offsetX];
            int index = neighbour.IndexOf(new Neighbour<TypeOfContent>(tile));
            if(index < 0)
            {
                neighbour.Add(new Neighbour<TypeOfContent>(tile));
                return;
            }
            else
            {
                neighbour[index].Count++;
                return;
            }
  
        }
    }
}
