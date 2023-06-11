using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace WFC
{
    internal class TileList<Type, TypeOfContent> : List<Type> where Type : Tile<TypeOfContent>
    {
        private List<Type> tiles;

        internal TileList(List<Tile<TypeOfContent>> tiles)
        {
            this.AddRange(tiles.GetRange(0,tiles.Count) as IEnumerable<Type>);
        }

        internal TileList()
        {


        }


        internal void RemoveAllExcept(Tile<TypeOfContent> tile)
        {
            if (!this.Contains(tile))
                return;
            int i = 0;
            while (this.Count != 1)
            {
                var item = this[i];
                if (item != tile)
                    this.Remove(item);
                else 
                    i++;
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
            if (this.Count == 1)
                return;

            NeighbourArray<TypeOfContent> neighbours = Tile.Neighbours;
            for (int i = 0; i < this.Count; i++)
            {
                var tile = this[i];
                if (!(neighbours[offsetY, offsetX].Contains(tile as Tile<TypeOfContent>)))
                {
                    Remove(tile);
                }
            }

        }
        /// <summary>
        /// Возвращает рандомный индекс с учетом весов 
        /// </summary>
        /// <param name="weight"> сумма всех весов должна быть == 1</param>
        /// <returns></returns>




    }
}
