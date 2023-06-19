using System;

// Класс для представления 2D вектора
namespace WFC
{
    public struct Vector2DInt
    {
        // Координаты вектора
        public int x;
        public int y;

        // Конструктор по умолчанию


        // Конструктор с параметрами
        public Vector2DInt( int y, int x)
        {
            this.x = x;
            this.y = y;
        }



        // Метод для вычисления скалярного произведения двух векторов
        public static int Dot(Vector2DInt a, Vector2DInt b)
        {
            return a.y * b.y + a.x * b.x; 
        }

        // Метод для вычисления векторного произведения двух векторов
        public static int Cross(Vector2DInt a, Vector2DInt b)
        {
            return a.y * b.x - a.x * b.y; 
        }

        // Метод для вычисления угла между двумя векторами в радианах

        // Метод для сложения двух векторов
        public static Vector2DInt Add(Vector2DInt a, Vector2DInt b)
        {
            return new Vector2DInt(a.y + b.y, a.x + b.x); 
        }

        // Метод для вычитания двух векторов
        public static Vector2DInt Subtract(Vector2DInt a, Vector2DInt b)
        {
            return new Vector2DInt(a.y - b.y, a.x - b.x); 
        }

        // Метод для умножения вектора на скаляр
        public static Vector2DInt Multiply(Vector2DInt a, int k)
        {
            return new Vector2DInt(a.y * k, a.x * k); 
        }

        // Метод для деления вектора на скаляр
        public static Vector2DInt Divide(Vector2DInt a, int k)
        {
            return new Vector2DInt(a.y / k, a.x / k); 
        }

        // Перегрузка оператора + для сложения двух векторов
        public static Vector2DInt operator +(Vector2DInt a, Vector2DInt b)
        {
            return Add(a, b);
        }

        // Перегрузка оператора - для вычитания двух векторов
        public static Vector2DInt operator -(Vector2DInt a, Vector2DInt b)
        {
            return Subtract(a, b);
        }

        public static bool operator ==(Vector2DInt a, Vector2DInt b)
        {
            return a.y == b.y && a.x == b.x;
        }
        public static bool operator !=(Vector2DInt a, Vector2DInt b)
        {
            return !(a == b);
        }
        // Перегрузка оператора * для умножения вектора на скаляр
        public static Vector2DInt operator *(Vector2DInt a, int k)
        {
            return Multiply(a, k);
        }

        // Перегрузка оператора / для деления вектора на скаляр
        public static Vector2DInt operator /(Vector2DInt a, int k)
        {
            return Divide(a, k);
        }
    }
}
