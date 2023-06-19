using static WFC.TileFunctions;

namespace WFC
{
    internal class NeighbourArray<TypeOfContent>
    {
        private TileList<Neighbour<TypeOfContent>,TypeOfContent>[,] neighbours;
        private Vector2DInt center; 

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
            center.y = neighbours.GetLength(0) / 2; 
            center.x = neighbours.GetLength(1) / 2;
            SpiralArrayTraversal(neighbours, center, (offset) =>
            {

                neighbours[offset.y, offset.x] = new TileList<Neighbour<TypeOfContent>, TypeOfContent>();
            });
            
        }

        internal TileList<Neighbour<TypeOfContent>, TypeOfContent>this[int y, int x]
        {
            get
            { 

                return neighbours[center.y + y, center.x + x];
            }
            set {

                neighbours[center.y + y, center.x + x] = value;
            }
        }
        internal TileList<Neighbour<TypeOfContent>, TypeOfContent> this[Vector2DInt point]
        {
            get
            {

                return neighbours[center.y + point.y, center.x + point.x];
            }
            set
            {

                neighbours[center.y + point.y, center.x + point.x] = value;
            }
        }
        internal int GetLength(int dimension)
        {
            return neighbours.GetLength(dimension);
        }
        /// <summary>
        /// Добавляет соседей
        /// </summary>
        /// <param name="neighboursTile"></param>
        /// <param name="offsetY">Положение относительно </param>
        /// <param name="offsetX">Положение относительно </param>
        internal void AddNeighbour(Tile<TypeOfContent> neighboursTile, int offsetY, int offsetX)
        {
            if(this[offsetY, offsetX] == null)
                this[offsetY, offsetX] = new TileList<Neighbour<TypeOfContent>, TypeOfContent>();

            TileList<Neighbour<TypeOfContent>, TypeOfContent> neighbour = this[offsetY, offsetX];
            int index = neighbour.IndexOf(new Neighbour<TypeOfContent>(neighboursTile));
            if(index < 0)
            {
                neighbour.Add(new Neighbour<TypeOfContent>(neighboursTile));
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
