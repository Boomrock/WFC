using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace WFC
{
    internal class TileList<Type, TypeOfContent> :List<Type> where Type:Tile<TypeOfContent>
    {
        private List<Type> tiles;

        internal TileList(List<Tile<TypeOfContent>> tiles)
        {
            this.tiles = tiles as List<Type>;
        }

        internal TileList()
        {
            this.tiles = new List<Type>();

        }

        internal void RemoveAllExcept(Tile<TypeOfContent> tile)
        {
            foreach (var item in this)
            {
                if (item != tile)
                    this.Remove(item);
            }

        }
        /// <summary>
        /// удаляет 
        /// </summary>
        /// <param name="Tile"></param>
        /// <param name="offsetY"></param>
        /// <param name="offsetX"></param>
        internal void DeleteUnpossibleTile(Tile<TypeOfContent> Tile, int offsetY, int offsetX)
        {
            //Смотрим на соседей точки ( int offsetY, int offsetX), координаты относительные 
            //Для соседней точки наша точка имеет координаты - ( int offsetY, int offsetX)
            //Смотрим каких соседей нет у соседней точки в нашей строчке
            //удаляем такие тайлы
            NeighbourArray<TypeOfContent> neighbours = Tile.Neighbours;
            foreach (var tile in this)
            {
                if (!neighbours[-offsetY, -offsetX].Contains(tile as Tile<TypeOfContent>))
                {
                    Remove(tile);
                }
            }

        }


    }
}
