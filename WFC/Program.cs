using System;


namespace WFC
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int[,] dimensions = new int[,]
            {
                   {-1, 0},
                   { 0,-1},
                   { 1, 0},
                   { 0, 1},
            };
            char[,] map = new char[,] {
            {'-', 'c', 'c', 'd', 'в', 'd'},
            {'c', 'T', 'T', 'c', 's', 'a'},
            {'d', 'c', 'c', 'x', 'T', 'O'},
            {'-', 'x', 'R', 'x', 'd', 'O'},
            {'D', 'x', 'S', 'x', 'a', '-'},
            {'-', 'a', 'x', 'S', 's', '-'}

            };
            WaveCollapsingFunction<char> function = new WaveCollapsingFunction<char>();
            Tile<char>[,] tiles = new Tile<char>[map.GetLength(0), map.GetLength(1)];
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int z = 0; z < map.GetLength(1); z++)
                {
                    tiles[i, z] = new Tile<char>(map[i, z]);
                }
            }
            function.Analysis(tiles);
            var newMap = function.Colaps(6, 6, 'S', '$');
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int z = 0; z < map.GetLength(1); z++)
                {
                    Console.Write(newMap[i, z]);
                }
                Console.WriteLine();

            }

        }
    }
}
