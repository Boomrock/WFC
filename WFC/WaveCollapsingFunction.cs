using System.Data.Common;
using static WFC.TileFunctions;
namespace WFC
{
    /*
    Смотрим по соседним клеткам
    Добавляем соседей 
    Вычисляем частоту 
     
     */
    internal class WaveCollapsingFunction<TypeOfContent> 
    {     
        int countLayers = 1;
        TileList<Tile<TypeOfContent>, TypeOfContent> tiles;
        public WaveCollapsingFunction()
        {
            tiles = new TileList<Tile<TypeOfContent>, TypeOfContent>();
            TileFunctions.countLayers = countLayers;
        }
        /// <summary>
        /// Генерация карты
        /// </summary>
        /// <param name="sizeY"></param>
        /// <param name="sizeX"></param>
        /// <returns></returns>
        public TypeOfContent[,] Colaps(int sizeY, int sizeX)
        {
            TileList<Tile<TypeOfContent>, TypeOfContent>[,] colapsMap = new TileList<Tile<TypeOfContent>, TypeOfContent>[sizeY, sizeX];
            for (int y = 0; y < sizeY; y++)
                for (int x = 0; x < sizeX; x++)
                    colapsMap[y, x] = tiles;
            int x_first = 2;
            int y_first = 2;

            SpiralArrayTraversal(colapsMap, y_first, x_first, (y,x )=>
            {
                var currentTile = colapsMap[y, x][0];
                LookAtNeighbour(colapsMap, y, x, (offsetY, offsetX) =>
                {
                     colapsMap[y,x].DeleteUnpossibleTile(currentTile, offsetY, offsetX );
                });
            }
            );
            TypeOfContent[,] contents = new TypeOfContent[colapsMap.GetLength(0), colapsMap.GetLength(1)]; 
            for (int y = 0; y < colapsMap.GetLength(0); y++)
                for (int x = 0; x < colapsMap.GetLength(1); x++)
                {
                    contents[y, x] = colapsMap[y, x][0].Content;
                }

            return contents;
        }
        /// <summary>
        /// Функция анализа, вроде закончена 
        /// </summary>
        /// <param name="arrayContents"></param>
        public void Analysis(Tile<TypeOfContent>[,] arrayContents)
        {
            for (int y = 0; y < arrayContents.GetLength(0); y++)
            {
                for (int x = 0; x < arrayContents.GetLength(1); x++)
                {
                    if (tiles.Contains(arrayContents[y, x]))
                    {
                        AddNeighbour(arrayContents, 0, y, x);

                        continue;
                    }
                    else
                    {
                        tiles.Add(arrayContents[y, x]);
                        AddNeighbour(arrayContents, 0, y, x);


                        continue;
                    }
                }

            }
        }
    }
}
