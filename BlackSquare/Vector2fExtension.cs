using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace BlackSquare
{
    /// <summary>
    /// Методы расширения для структуры Vector2f.
    /// </summary>
    internal static class Vector2fExtension
    {
        public static Vector2f Mul(this Vector2f a, Vector2f b)
        {
            return new Vector2f(a.X * b.X, a.Y * b.Y);
        }

        public static Vector2f Mul(this Vector2f a, Vector2i b)
        {
            return new Vector2f(a.X * b.X, a.Y * b.Y);
        }

        public static Vector2f Mul(this Vector2f a, float b)
        {
            return new Vector2f(a.X * b, a.Y * b);
        }

        public static Vector2f Div(this Vector2f a, Vector2f b)
        {
            return new Vector2f(a.X / b.X, a.Y / b.Y);
        }

        public static Vector2f Div(this Vector2f a, Vector2i b)
        {
            return new Vector2f(a.X / b.X, a.Y / b.Y);
        }

        public static Vector2f Add(this Vector2f a, Vector2f b)
        {
            return new Vector2f(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2f Div(this Vector2f a, float b)
        {
            return new Vector2f(a.X / b, a.Y / b);
        }

        public static Vector2f Add(this Vector2f a, Vector2i b)
        {
            return new Vector2f(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2f Sub(this Vector2f a, Vector2f b)
        {
            return new Vector2f(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2f Sub(this Vector2f a, Vector2i b)
        {
            return new Vector2f(a.X - b.X, a.Y - b.Y);
        }

        public static bool Equal(this Vector2f a, Vector2f b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool Equal(this Vector2f a, Vector2i b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool Lt(this Vector2f a, Vector2f b)
        {
            if (a.X == b.X) return a.Y < b.Y;
            return a.X < b.X;
        }

        public static bool Lt(this Vector2f a, Vector2i b)
        {
            if (a.X == b.X) return a.Y < b.Y;
            return a.X < b.X;
        }

        public static bool Gt(this Vector2f a, Vector2f b)
        {
            if (a.X == b.X) return a.Y > b.Y;
            return a.X > b.X;
        }

        public static bool Gt(this Vector2f a, Vector2i b)
        {
            if (a.X == b.X) return a.Y > b.Y;
            return a.X > b.X;
        }

        public static bool  LtEq(this Vector2f a, Vector2f b)
        {
            if (a.X == b.X) return a.Y <= b.Y;
            return a.X <= b.X;
        }

        public static bool  LtEq(this Vector2f a, Vector2i b)
        {
            if (a.X == b.X) return a.Y <= b.Y;
            return a.X <= b.X;
        }

        public static bool GtEq(this Vector2f a, Vector2f b)
        {
            if (a.X == b.X) return a.Y >= b.Y;
            return a.X >= b.X;
        }

        public static bool GtEq(this Vector2f a, Vector2i b)
        {
            if (a.X == b.X) return a.Y >= b.Y;
            return a.X >= b.X;
        }

        public static float Length(this Vector2f a)
        {
            return (float)Math.Sqrt(a.X * a.X + a.Y * a.Y);
        }

        public static float VMul(this Vector2f a, Vector2f b)
        {
            return a.X * b.Y - a.Y * b.X;
        }
    }
}
