using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Threading;

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
            {'0', '0', '0', '0', '0', '0'},
            {'c', 'T', 'T', 'c', '0', '0'},
            {'0', 'c', 'c', '0', 'T', 'O'},
            {'0', '0', 'x', '0', 'T', 'O'},
            {'D', '0', '0', '0', '0', '0'},
            {'0', '0', 'S', 'S', 'S', '0'}

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



        }

     
    }
}
