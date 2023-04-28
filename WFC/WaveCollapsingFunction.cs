using System;
using System.Collections.Generic;
using System.Text;

namespace WFC
{
    internal class WaveCollapsingFunction<TypeOfContent>
    {       //                              top     left     down     right     
        int[,] direction = new int[,] { { -1, 0 },{ 0, -1}, { 1,0}, { 0, 1} };
        List<Tile<TypeOfContent>> tiles;
        public WaveCollapsingFunction()
        {
            tiles = new List<Tile<TypeOfContent>>();
        }
        public TypeOfContent[,] Colaps(int sizeY , int sizeX )
        {
            TypeOfContent[,] contents = new TypeOfContent[sizeX, sizeY];
            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {

                }
            }

        } 
        public void Analysis(Tile<TypeOfContent>[,] arrayContents)
        {
            for (int y = 0; y < arrayContents.GetLength(0); y++)
            {
                for (int x = 0; x < arrayContents.GetLength(1); x++)
                {
                    if (tiles.Contains(arrayContents[y,x]))
                    {
                        AddNeighbour(arrayContents,0, y, x);
                        continue;
                    }
                    else
                    {
                        tiles.Add(arrayContents[y,x]);
                        AddNeighbour(arrayContents, 0, y, x);
                        continue;
                    }
                }
                
            }
            Console.WriteLine("Analysis complete. Unique tile:" + tiles.Count);
        }

        private void AddNeighbour(Tile<TypeOfContent>[,] arrayContents, int indexLevel, int y, int x)
        {
            int indexTile = tiles.IndexOf(arrayContents[y, x]);
            if (indexTile == -1)
                throw new Exception("AddNeighbour: tiles не содержит элемента из arrayContents");

            for (int i = 0; i < direction.GetLength(0); i++)
            {
                int offsetY= y + direction[i, 0];
                int offsetX = x + direction[i, 1]; 
                if (!(offsetX > 0 &&
                    offsetY > 0 &&
                    offsetX < arrayContents.GetLength(1) &&
                    offsetY < arrayContents.GetLength(0)))
                    return;
                tiles[indexTile].Levels[indexLevel].AddNeighbour(arrayContents[y, x], (Direction)i);
            }
        }
    }
}
