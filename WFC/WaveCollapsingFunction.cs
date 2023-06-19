using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
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
        public TypeOfContent[,] Generate(Vector2DInt size, TypeOfContent content, TypeOfContent Defualt,Vector2DInt CenterColaps)
        {
            //готово
            TileList<Tile<TypeOfContent>, TypeOfContent>[,] colapsMap = GetColapsMap(size);
            //готово
            colapsMap[CenterColaps.y, CenterColaps.x].RemoveAllExcept(new Tile<TypeOfContent>(content));
            Colaps(colapsMap, CenterColaps);
            //не готово 
            PlaceTile(colapsMap, CenterColaps);
            //не готово
            TypeOfContent[,] contents = GetContentsMap(Defualt, colapsMap);
            return contents;


        }

        private void PlaceTile(TileList<Tile<TypeOfContent>, TypeOfContent>[,] colapsMap, Vector2DInt centerColaps)
        {
            SpiralArrayTraversal(colapsMap, centerColaps, (point) =>
            {
                ColapsTile(colapsMap, point);
            });
        }

        private static  void ColapsTile(TileList<Tile<TypeOfContent>, TypeOfContent>[,] colapsMap, Vector2DInt point)
        {

            var tileList = colapsMap[point.y, point.x];
            if(tileList.Count > 0)
                tileList.RemoveAllExcept(tileList[GetRandomTile(tileList.Count)]);

            //var tileList = colapsMap[point.y, point.x];
            //int[] count = new int[tileList.Count];

            //SpiralArrayTraversal(colapsMap, point, (offset) =>
            //{
            //    var offsetPoint = offset - point ;
            //    if (offsetPoint.x == 0  && offsetPoint.x == 0)
            //        return;
            //    for (int i = 0; i < count.Length; i++)
            //    {
            //        var tile = tileList[i];
            //        foreach (var neighbour in colapsMap[offset.y, offset.x])
            //        {
            //            int index = neighbour.Neighbours[offsetPoint].IndexOf(new Neighbour<TypeOfContent>(tile));
            //            if (index != -1)
            //            {
            //                count[i] += neighbour.Neighbours[offsetPoint][index].Count;
            //            }
            //        }
            //    }


            //}, LayerCount);
            //int randomIndex = GetRandomIndexByWeights(count);
        }

        private TileList<Tile<TypeOfContent>, TypeOfContent>[,] GetColapsMap(Vector2DInt size)
        {
            TileList<Tile<TypeOfContent>, TypeOfContent>[,] colapsMap = new TileList<Tile<TypeOfContent>, TypeOfContent>[size.y, size.x];
            for (int y = 0; y < size.y; y++)
                for (int x = 0; x < size.x; x++)
                {
                        colapsMap[y, x] = new TileList<Tile<TypeOfContent>, TypeOfContent>(tiles.GetRange(0, tiles.Count));
                }

            return colapsMap;
        }
        private static TypeOfContent[,] GetContentsMap(TypeOfContent Defualt, TileList<Tile<TypeOfContent>, TypeOfContent>[,] colapsMap)
        {

            TypeOfContent[,] contents = new TypeOfContent[colapsMap.GetLength(0), colapsMap.GetLength(1)];
            SpiralArrayTraversal(colapsMap,new Vector2DInt(), (offset) =>
            {
                if(colapsMap[offset.y, offset.x].Count > 1) 
                    throw new Exception();
                
                if (colapsMap[offset.y, offset.x].Count == 1)
                    contents[offset.y, offset.x] = colapsMap[offset.y, offset.x][0].Content;
                else
                    contents[offset.y, offset.x] = Defualt;
            });
            return contents;
        }
        public static int i = 0;
        private static void Colaps(TileList<Tile<TypeOfContent>, TypeOfContent>[,] colapsMap, Vector2DInt positionsTile)
        {
            Queue<Vector2DInt> queue = new Queue<Vector2DInt>();
            AddNeighbourToQueue(colapsMap, queue, positionsTile);
            TileList<Tile<TypeOfContent>, TypeOfContent> posibleTile;
            while (queue.Count > 0)
            {
                positionsTile = queue.Dequeue();
                posibleTile = colapsMap[positionsTile.y, positionsTile.x];
                int countRemoved = posibleTile.RemoveAll(t => !IsTilePossible(t, positionsTile, colapsMap));
                if (countRemoved > 0) AddNeighbourToQueue(colapsMap, queue, positionsTile);
            }

            int maxCountTiles = colapsMap[0, 0].Count;
            Vector2DInt pointMaxCount = new Vector2DInt();
            SpiralArrayTraversal(colapsMap, new Vector2DInt(), (point) =>
            {
                if (maxCountTiles < colapsMap[point.y, point.x].Count) {
                    maxCountTiles = colapsMap[point.y, point.x].Count;
                    pointMaxCount = point;
                }
            });
            if (maxCountTiles == 1) return;
            ColapsTile(colapsMap, pointMaxCount);
            Colaps(colapsMap, pointMaxCount);
        }

        private static bool IsTilePossible(Tile<TypeOfContent> tile, Vector2DInt positionsTile, TileList<Tile<TypeOfContent>, TypeOfContent>[,] colapsMap)
        {
            bool isTilePossible = true;
            SpiralArrayTraversal(colapsMap, positionsTile, (positionsNeighbour) =>
            {
                isTilePossible &= CanPlaceTile(positionsTile, tile, colapsMap[positionsNeighbour.y, positionsNeighbour.x], positionsNeighbour);
            }, LayerCount);
            return isTilePossible;
        }
        private static bool CanPlaceTile(Vector2DInt positionsTile, Tile<TypeOfContent> tile, TileList<Tile<TypeOfContent>, TypeOfContent> Neighbours, Vector2DInt positionsNeighbour)
        {
            var offset = positionsNeighbour - positionsTile;
            if (offset.y == 0 && offset.x == 0)
                return true;
            return Neighbours.Any(neighbour => neighbour.Neighbours[-offset.y, -offset.x].Contains(tile));
        }
        //переделать
        private static void AddNeighbourToQueue(TileList<Tile<TypeOfContent>, TypeOfContent>[,] colapsMap, Queue<Vector2DInt> queue, Vector2DInt positionsTile)
        {
            SpiralArrayTraversal(colapsMap, positionsTile, (positionsTile)=>
            {
                queue.Enqueue(positionsTile);
            }, LayerCount);
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
                        AddNeighbour(arrayContents, tiles[index], new Vector2DInt(y,x));
                        continue;
                    }
                    else
                    {
                        tiles.Add(arrayContents[y, x]);
                        AddNeighbour(arrayContents, tiles[tiles.Count - 1], new Vector2DInt(y, x));
                        continue;
                    }
                }

            }
        }
    }

    
}
