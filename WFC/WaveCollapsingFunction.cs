using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFC
{
    internal class WaveCollapsingFunction<TypeOfContent>
    {       //                              top     left     down     right     
        int[,] direction = new int[,] { { -1, 0 }, { 0, -1 }, { 1, 0 }, { 0, 1 } };
        List<Tile<TypeOfContent>> tiles;
        public WaveCollapsingFunction()
        {
            tiles = new List<Tile<TypeOfContent>>();
        }
        public TypeOfContent[,] Colaps(int sizeY, int sizeX)
        {
            TypeOfContent[,] contents = new TypeOfContent[sizeY, sizeX];
            Random random = new Random();
            int YFirstContent = random.Next(0, contents.GetLength(0));
            int XFirstContent = random.Next(0, contents.GetLength(1));
            Tile<TypeOfContent> tile = tiles.Find(i => i.Equals(new Tile<char>('R')));

            contents[0, 0] = GetElement(tile);

            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    tile = tiles.Find(i => i.Equals(new Tile<TypeOfContent>(contents[y, x])));
                    List<Neighbour<TypeOfContent>> Neighbours = new List<Neighbour<TypeOfContent>>();// соседи точки 
                    if (tile == null)
                        continue;
                    
                    for (int i = 0; i < direction.GetLength(0); i++)
                    {
                        int offsetY = y + direction[i, 0];
                        int offsetX = x + direction[i, 1];
                        if (!(offsetX >= 0 &&
                            offsetY >= 0 &&
                            offsetX < contents.GetLength(1) &&
                            offsetY < contents.GetLength(0)))
                            continue;
                        int index = tiles.FindIndex(i => i == new Tile<TypeOfContent>(contents[offsetY, offsetX]));
                        if (index != -1)
                            Neighbours.Add(new Neighbour<TypeOfContent>(tiles[index]));




                    }
                    for (int i = 0; i < direction.GetLength(0); i++)
                    {
                        int offsetY = y + direction[i, 0];
                        int offsetX = x + direction[i, 1];
                        if (!(offsetX >= 0 &&
                            offsetY >= 0 &&
                            offsetX < contents.GetLength(1) &&
                            offsetY < contents.GetLength(0)))
                            continue;
                        TypeOfContent content = GetElement(tile, Neighbours);
                        if (content.Equals(default(TypeOfContent)) && contents[offsetY, offsetX] != null)
                            continue;
                        contents[offsetY, offsetX] = content;



                    }

                    
                }
            }
           
            
            return contents;

        }
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
            LogAnalys();
        }
        private void AddNeighbour(Tile<TypeOfContent>[,] arrayContents, int indexLevel, int y, int x)
        {
            int indexTile = tiles.IndexOf(arrayContents[y, x]);
            if (indexTile == -1)
                throw new Exception("AddNeighbour: tiles не содержит элемента из arrayContents");

            for (int i = 0; i < direction.GetLength(0); i++)
            {
                int offsetY = y + direction[i, 0];
                int offsetX = x + direction[i, 1];
                if (!(offsetX > 0 &&
                    offsetY > 0 &&
                    offsetX < arrayContents.GetLength(1) &&
                    offsetY < arrayContents.GetLength(0)))
                    continue;
                tiles[indexTile].Levels[indexLevel].AddNeighbour(arrayContents[offsetY, offsetX], (Direction)i);
            }
            foreach (var tile in tiles)
            {
                foreach (var level in tile.Levels)
                {
                    foreach (var neighbourside in level.Neighbours)
                    {
                        Neighbour<TypeOfContent>.CalculateFrequency(neighbourside);
                    }

                }

            }
        }

        private void LogAnalys()
        {
            Console.WriteLine("Analysis complete. Unique tile:" + tiles.Count);
            var tile = tiles.Find(i => i.Equals(new Tile<char>('x')));
            Console.WriteLine("For: " + tile.Content.ToString());
            foreach (var Neighbour in tile.Levels[0].Neighbours)
            {
                foreach (var item in Neighbour)
                {
                    Console.Write(item.Content.ToString() + " ");
                }
  

                Console.WriteLine();
            }
        }




        private TypeOfContent GetElement(Tile<TypeOfContent> tile, List<Neighbour<TypeOfContent>> Neighbours = null )
        {
            Random random = new Random();
            if (tile == null)
                return default;
            if (Neighbours == null)
                return tiles[random.Next(0, tiles.Count)].Content;

            List<Neighbour<TypeOfContent>> PosibleTiles = new List<Neighbour<TypeOfContent>>();

            foreach (var level in tile.Levels)
            {
                foreach (var side in level.Neighbours)
                {
                    foreach (var neighbour in side)
                    {

                        bool TileIsPosible = Neighbours.All(_neighbour =>
                                                _neighbour.Levels.All(
                                                            level =>
                                                                level.Neighbours.Any(
                                                                        side => side.Contains(neighbour))
                                                            ));

                        if (TileIsPosible && 
                           !PosibleTiles.Contains(neighbour))
                        {
                            PosibleTiles.Add(neighbour);
                        }
                   

                    }

                }
            }


            float percentage = (float)random.NextDouble();
            float currentPercentage = 0;
            foreach (var posibleTile in PosibleTiles)
            {
                currentPercentage += posibleTile.Frequency;
                if(currentPercentage >= percentage)
                {
                    return posibleTile.Content;
                }
            }
            return default;

        }
    }
}
