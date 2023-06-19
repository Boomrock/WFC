using System;


namespace WFC
{
    internal class Program
    {
        static void Main(string[] args)
        {

            char[,] map = new char[,] {
            {'-', '-', '-', '-', '-', '-'},
            {'-', '-', '-', 'c', '-', '-'},
            {'-', 'c', 'c', 'x', 'c', '-'},
            {'c', 'x', 'x', 'x', 'x', 'c'},
            {'x', 'x', 'x', 'x', 'x', 'x'},
            {'x', 'x', 'x', 'x', 'x', 'x'}

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

            var newMap = function.Generate(new Vector2DInt(30, 30), '-', '*', new Vector2DInt(15,15)  );
            for (int i = 0; i < newMap.GetLength(0); i++)
            {
                for (int z = 0; z < newMap.GetLength(1); z++)
                {
                    Console.Write(newMap[i, z]);
                }
                Console.WriteLine();

            }

        }
    }
}
