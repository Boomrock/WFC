using System;
using System.Collections.Generic;
using System.IO;

using System.Text;

namespace WFC
{
    internal class Tile<TypeOfContent>
    {
        TypeOfContent content;
        List<Level<TypeOfContent>> levels;
        public byte[] hash;

        public TypeOfContent Content { get => content; set => content = value; }
        internal List<Level<TypeOfContent>> Levels { get => levels; set => levels = value; }

        public Tile(TypeOfContent content)
        {
            this.content = content;
            this.hash =  HashUtils.GetHashObject(content);
            this.levels = new List<Level<TypeOfContent>>();
            this.levels.Add(new Level<TypeOfContent>());
            if(hash.Length > 16)
            {
                throw new Exception("в Tile длинна хэша больше 16");
            }
        }

        public static bool operator ==(Tile<TypeOfContent> left, Tile<TypeOfContent> right)
        {

            return left.Equals(right);
        }
        public static bool operator !=(Tile<TypeOfContent> left, Tile<TypeOfContent> right)
        {
            return left.Equals(right);
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
