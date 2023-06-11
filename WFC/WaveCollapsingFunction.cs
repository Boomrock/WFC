using System;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;
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
        private const int LayerCount = 1;
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
        public TypeOfContent[,] Colaps(int sizeY, int sizeX, TypeOfContent content, TypeOfContent Defualt, int centerColapsY = 2,  int centerColapsX = 2 )
        {

            TileList<Tile<TypeOfContent>, TypeOfContent>[,] colapsMap = GetColapsMap(sizeY, sizeX);
            colapsMap[centerColapsY, centerColapsX].RemoveAllExcept(new Tile<TypeOfContent>(content));
            DeleteUnpossibleTileInMap(colapsMap, centerColapsY, centerColapsX);
            TypeOfContent[,] contents = GetContentsMap(Defualt, colapsMap);
            return contents;


        }
        private TileList<Tile<TypeOfContent>, TypeOfContent>[,] GetColapsMap(int sizeY, int sizeX)
        {
            TileList<Tile<TypeOfContent>, TypeOfContent>[,] colapsMap = new TileList<Tile<TypeOfContent>, TypeOfContent>[sizeY, sizeX];
            for (int y = 0; y < sizeY; y++)
                for (int x = 0; x < sizeX; x++)
                {
                    colapsMap[y, x] = new TileList<Tile<TypeOfContent>, TypeOfContent>(tiles.GetRange(0, tiles.Count));
                }

            return colapsMap;
        }
        private static TypeOfContent[,] GetContentsMap(TypeOfContent Defualt, TileList<Tile<TypeOfContent>, TypeOfContent>[,] colapsMap)
        {

            TypeOfContent[,] contents = new TypeOfContent[colapsMap.GetLength(0), colapsMap.GetLength(1)];
            SpiralArrayTraversal(colapsMap, 0, 0, (y, x) =>
            {
                var tileList = colapsMap[y, x];
                int[] count = new int[tileList.Count];
                var currentTile = colapsMap[y, x][0];

                SpiralArrayTraversal(colapsMap, y, x, (offsetY, offsetX) =>
                {
                    
                    if (offsetX - x + offsetY - y == 0)
                        return;
                    for (int i = 0; i < count.Length; i++)
                    {
                        var tile = tileList[i];
                        foreach (var neighbour in colapsMap[offsetX, offsetY])
                        {
                            int index = neighbour.Neighbours[offsetY - y, offsetX - x].IndexOf(new Neighbour<TypeOfContent>(tile));
                            if (index != -1)
                            {
                                count[i] += neighbour.Neighbours[offsetY - y, offsetX - x][index].Count;
                            }
                        }
                    }


                }, LayerCount);
                int randomIndex = GetRandomIndexByWeights(count);
                tileList.RemoveAllExcept(tileList[randomIndex]);


                if (colapsMap[y, x].Count > 0)
                    contents[y, x] = colapsMap[y, x][0].Content;
                else
                    contents[y, x] = Defualt;
            });


            return contents;
        }

        private static void DeleteUnpossibleTileInMap(TileList<Tile<TypeOfContent>, TypeOfContent>[,] colapsMap, int centerY, int centerX)
        {
            SpiralArrayTraversal(colapsMap, centerY, centerX, (y, x) =>
            {
                var currentTile = colapsMap[y, x][0];
                SpiralArrayTraversal(colapsMap, y, x, (offsetY, offsetX) =>
                {
                    if (offsetY - y + offsetX - x != 0)
                        colapsMap[offsetY, offsetX].DeleteUnpossibleTile(currentTile, offsetY - y, offsetX - x);
                }, LayerCount);
            });
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
                    int index = tiles.IndexOf(arrayContents[y, x]);
                    if (index >= 0 )
                    {
                        AddNeighbour(arrayContents, tiles[index], y, x);
                        continue;
                    }
                    else
                    {
                        tiles.Add(arrayContents[y, x]);
                        AddNeighbour(arrayContents, tiles[tiles.Count - 1], y, x);
                        continue;
                    }
                }

            }
        }
    }
}
