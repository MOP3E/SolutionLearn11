using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace BlackSquare
{
    /// <summary>
    /// Методы расширения для структуры Vector2i.
    /// </summary>
    internal static class Vector2iExtension
    {
        public static Vector2i Mul(this Vector2i a, Vector2i b)
        {
            return new Vector2i(a.X * b.X, a.Y * b.Y);
        }

        public static Vector2i Mul(this Vector2i a, Vector2f b)
        {
            return new Vector2i(a.X * (int)b.X, a.Y * (int)b.Y);
        }

        public static Vector2i Mul(this Vector2i a, int b)
        {
            return new Vector2i(a.X * b, a.Y * b);
        }

        public static Vector2i Div(this Vector2i a, Vector2i b)
        {
            return new Vector2i(a.X / b.X, a.Y / b.Y);
        }

        public static Vector2i Div(this Vector2i a, Vector2f b)
        {
            return new Vector2i(a.X / (int)b.X, a.Y / (int)b.Y);
        }

        public static Vector2i Div(this Vector2i a, int b)
        {
            return new Vector2i(a.X / b, a.Y / b);
        }

        public static Vector2i Add(this Vector2i a, Vector2i b)
        {
            return new Vector2i(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2i Add(this Vector2i a, Vector2f b)
        {
            return new Vector2i(a.X + (int)b.X, a.Y + (int)b.Y);
        }

        public static Vector2i Sub(this Vector2i a, Vector2i b)
        {
            return new Vector2i(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2i Sub(this Vector2i a, Vector2f b)
        {
            return new Vector2i(a.X - (int)b.X, a.Y - (int)b.Y);
        }

        public static bool Equal(this Vector2i a, Vector2i b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool Equal(this Vector2i a, Vector2f b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool Lt(this Vector2i a, Vector2i b)
        {
            if (a.X == b.X) return a.Y < b.Y;
            return a.X < b.X;
        }

        public static bool Lt(this Vector2i a, Vector2f b)
        {
            if (a.X == b.X) return a.Y < b.Y;
            return a.X < b.X;
        }

        public static bool Gt(this Vector2i a, Vector2i b)
        {
            if (a.X == b.X) return a.Y > b.Y;
            return a.X > b.X;
        }

        public static bool Gt(this Vector2i a, Vector2f b)
        {
            if (a.X == b.X) return a.Y > b.Y;
            return a.X > b.X;
        }

        public static bool  LtEq(this Vector2i a, Vector2i b)
        {
            if (a.X == b.X) return a.Y <= b.Y;
            return a.X <= b.X;
        }

        public static bool  LtEq(this Vector2i a, Vector2f b)
        {
            if (a.X == b.X) return a.Y <= b.Y;
            return a.X <= b.X;
        }

        public static bool GtEq(this Vector2i a, Vector2i b)
        {
            if (a.X == b.X) return a.Y >= b.Y;
            return a.X >= b.X;
        }

        public static bool GtEq(this Vector2i a, Vector2f b)
        {
            if (a.X == b.X) return a.Y >= b.Y;
            return a.X >= b.X;
        }
    }
}
