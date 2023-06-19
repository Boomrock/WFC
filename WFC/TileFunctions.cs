using System;
using System.Collections.Generic;

namespace WFC
{
    internal class TileFunctions
    {
        internal static int countLayers = 1;
        static Random random = new Random();
        public static int GetRandomIndexByWeights(int[] count)
        {
            if (count.Length == 0)
                return 0;
            float sum = 0;
            float[] weight = new float[count.Length];
            for (int z = 0; z < count.Length; z++) sum += count[z];
            for (int r = 0; r < count.Length; r++) weight[r] = (float)count[r] / sum;
            float percent = (float)random.NextDouble();
            float currentPercent = 0;
            int i = 0;
            while (percent > currentPercent)
            {
                currentPercent += weight[i];
                i++;
            }
            return i - 1;

        }
        public static int GetRandomTile(int Count)
        {
            return random.Next(Count);
        }
        /// <summary>
        /// добавляет соседей 
        /// </summary>
        /// <typeparam name="TypeOfContent">тип контента</typeparam>
        /// <param name="arrayContents"></param>
        /// <param name="tile">кому добавляем соседей</param>
        /// <param name="y">координата tile</param>
        /// <param name="x">координата tile</param>
        internal static void AddNeighbour<TypeOfContent>(Tile<TypeOfContent>[,] arrayContents, Tile<TypeOfContent>  tile, Vector2DInt point)
        {
            SpiralArrayTraversal(arrayContents, point, (pointNeghbour) =>
            {
                var offset = pointNeghbour - point;
                if(offset != new Vector2DInt(0,0))
                tile.Neighbours.AddNeighbour(arrayContents[pointNeghbour.y, pointNeghbour.x], offset.y, offset.x);// 
            }, 1);
        }  
   /// <summary>
   /// Проходит все точки вокруг (y,x) по спирали
   /// </summary>
   /// <typeparam name="type"></typeparam>
   /// <param name="arrayContents"></param>
   /// <param name="y"></param>
   /// <param name="x"></param>
   /// <param name="LayersCount"></param>
   /// <param name="action"></param>
   /// <param name=""></param>
        internal static void SpiralArrayTraversal<type>(type[,] arrayContents, Vector2DInt position, Action<Vector2DInt> action, int LayerCount = -1)
        {
            // Создаем очередь для хранения координат элементов, которые нужно посетить
            Queue<(int, int)> queue = new Queue<(int, int)>();
            // Создаем массив для отметки элементов, которые уже были посещены
            bool[,] visited = new bool[arrayContents.GetLength(0), arrayContents.GetLength(1)];
            // Добавляем точку p в очередь и отмечаем ее как посещенную
            queue.Enqueue((position.y, position.x));
            visited[position.y, position.x] = true;
            // Пока очередь не пуста
            while (queue.Count > 0)
            {

                // Извлекаем первый элемент из очереди
                (position.y, position.x) = queue.Dequeue();
                // Делаем что-то с этим элементом
                action(position);
                // Проходим по всем четырем направлениям от этого элемента
                int[] dx = { 0, 0, -1, 1, -1, 1, -1, 1}; // Смещение по x
                int[] dy = { -1, 1, 0, 0, -1, 1, 1, -1 }; // Смещение по y

                if (LayerCount != 0)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        // Вычисляем координаты соседнего элемента
                        int nx = position.x + dx[k];
                        int ny = position.y + dy[k];
                        // Проверяем, что они в пределах массива и не были посещены
                        if (nx >= 0 && nx < arrayContents.GetLength(1) && ny >= 0 && ny < arrayContents.GetLength(0) && !visited[ny, nx])
                        {
                            // Добавляем соседний элемент в очередь и отмечаем его как посещенный
                            queue.Enqueue((ny, nx));
                            visited[ny, nx] = true;
                        }
                    }
                    LayerCount--;
                }
            }
        }
      
    }
}