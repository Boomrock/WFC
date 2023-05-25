using System;
using System.Collections.Generic;

namespace WFC
{
    internal class TileFunctions
    {
        internal static int countLayers = 1; 
        /// <summary>
        /// добавляет соседей 
        /// </summary>
        /// <typeparam name="TypeOfContent"></typeparam>
        /// <param name="arrayContents"></param>
        /// <param name="indexLevel"></param>
        /// <param name="y"></param>
        /// <param name="x"></param>
        internal static void AddNeighbour<TypeOfContent>(Tile<TypeOfContent>[,] arrayContents, int indexLevel, int y, int x)
        {
            var tile = arrayContents[y, x];
            LookAtNeighbour(arrayContents, y, x, (offsetY, offsetX) =>
            {
                tile.Neighbours.AddNeighbour(arrayContents[y, x], offsetY - y, offsetX - x );
            });
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
        internal static void SpiralArrayTraversal<type>(type[,] arrayContents, int y, int x, Action<int, int> action)
        {
            // Создаем очередь для хранения координат элементов, которые нужно посетить
            Queue<(int, int)> queue = new Queue<(int, int)>();
            // Создаем массив для отметки элементов, которые уже были посещены
            bool[,] visited = new bool[arrayContents.GetLength(0), arrayContents.GetLength(1)];
            // Добавляем точку p в очередь и отмечаем ее как посещенную
            queue.Enqueue((y, x));
            visited[y, x] = true;
            // Пока очередь не пуста
            while (queue.Count > 0)
            {
                // Извлекаем первый элемент из очереди
                (x , y) = queue.Dequeue();
                // Делаем что-то с этим элементом
                action(y, x);
                // Проходим по всем четырем направлениям от этого элемента
                int[] dx = { 0, 0, -1, 1 }; // Смещение по x
                int[] dy = { -1, 1, 0, 0 }; // Смещение по y
                for (int k = 0; k < 4; k++)
                {
                    // Вычисляем координаты соседнего элемента
                    int nx = x + dx[k];
                    int ny = y + dy[k];
                    // Проверяем, что они в пределах массива и не были посещены
                    if (nx >= 0 && nx < arrayContents.GetLength(1) && ny >= 0 && ny < arrayContents.GetLength(0) && !visited[ny, nx])
                    {
                        // Добавляем соседний элемент в очередь и отмечаем его как посещенный
                        queue.Enqueue((ny,nx));
                        visited[ny, nx] = true;
                    }
                }
            }
        }
        internal static void LookAtNeighbour<type>(type[,] arrayContents, int y, int x, Action<int, int> action)
        {

            for (int _y = -countLayers; _y <= countLayers; _y++)
            {

                for (int _x = -countLayers; _x <= countLayers; _x++)
                {
                    if (_x == 0 && _y == 0)
                        continue;
                    int offsetY = y + _x;
                    int offsetX = x + _x;
                    if (!(offsetX >= 0 &&
                        offsetY >= 0 &&
                        offsetX < arrayContents.GetLength(1) &&
                        offsetY < arrayContents.GetLength(0)))
                        continue;
                    action(offsetY, offsetX);
                }

            }

        }
        internal static void LookAtNeighbour<TypyOfContent>(NeighbourArray<TypyOfContent> arrayContents, int y, int x, Action<int, int> action)
        {

            for (int _y = -countLayers; _y <= countLayers; _y++)
            {

                for (int _x = -countLayers; _x <= countLayers; _x++)
                {

                    int offsetY = y + _x;
                    int offsetX = x + _x;
                    if (offsetX == 0 && offsetY == 0)
                        continue;
                    if (!(offsetX > 0 &&
                        offsetY > 0 &&
                        offsetX < arrayContents.GetLength(1) &&
                        offsetY < arrayContents.GetLength(0)))
                        continue;
                    action(offsetY, offsetX);
                }

            }

        }
    }
}