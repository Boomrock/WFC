using System;
using System.Collections.Generic;
using System.IO;

using System.Text;

namespace WFC
{
    internal class Tile<TypeOfContent>
    {
        TypeOfContent content;
        NeighbourArray<TypeOfContent> neighbours;
        public byte[] hash;

        public TypeOfContent Content { get => content; set => content = value; }
        internal NeighbourArray<TypeOfContent> Neighbours { get => neighbours; set => neighbours = value; }


        public Tile(Tile<TypeOfContent> tile)
        {
            this.neighbours = tile.neighbours;
            this.content = tile.content;
            this.hash = tile.hash;
        }
        public Tile(TypeOfContent content)
        {
            this.content = content;
            this.hash =  HashUtils.GetHashObject(content);
            this.neighbours = new NeighbourArray<TypeOfContent>(1);
            if(hash.Length > 16)
            {
                throw new Exception("в Tile длинна хэша больше 16");
            }
        }

        public static bool operator ==(Tile<TypeOfContent> left, Tile<TypeOfContent> right)
        {
            if (left is null)
                return true;
            return left.Equals(right);

        }
        public static bool operator !=(Tile<TypeOfContent> left, Tile<TypeOfContent> right)
        {
            return !left.Equals(right);
        }
        public override bool Equals(object obj)
        {
            bool Equal = false;
            if (obj is Tile<TypeOfContent> tile && 
                tile.hash.Length == this.hash.Length)
            {
                int i = 0;
                while ((i < tile.hash.Length) && (tile.hash[i] == this.hash[i]))
                {
                    i += 1;
                }
                if (i == tile.hash.Length)
                {
                    Equal = true;
                }
            }
            return Equal;
        }

        public override int GetHashCode()
        {
            return HashUtils.ByteToInt(hash);
        }
    }
        //тайл может в себе содержать соседей 
        //тайл должен содержать хэш или уникальный код контента, что бы определить принадлежность соседа к контенту 
    
}
