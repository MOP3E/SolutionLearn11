using SFML.Graphics;
using SFML.System;

namespace BlackSquare
{
    /// <summary>
    /// Подвижный квадрат.
    /// </summary>
    internal class Square
    {
        /// <summary>
        /// Пи
        /// </summary>
        private const float Pi = (float)(Math.PI);
        
        /// <summary>
        /// 1/2 Пи
        /// </summary>
        private const float Pi12 = (float)(Math.PI * .5);
        
        /// <summary>
        /// 3/2 Пи
        /// </summary>
        private const float Pi32 = (float)(Math.PI * 1.5);

        /// <summary>
        /// Коэффициент размера треугольника - косинус.
        /// </summary>
        private readonly float TriangleFactorCos = (float)(Math.Cos(Math.PI / 6.0));

        /// <summary>
        /// Коэффициент размера треугольника - синус.
        /// </summary>
        private readonly float TriangleFactorSin = (float)(Math.Sin(Math.PI / 6.0));

        /// <summary>
        /// Размер квадрата.
        /// </summary>
        protected int Size;

        /// <summary>
        /// Положение центра квадрата на экране.
        /// </summary>
        protected Vector2f Position;

        /// <summary>
        /// Приращение координат квадрата за одну секунду.
        /// </summary>
        protected Vector2f Increment;

        /// <summary>
        /// Точка назначения квадрата.
        /// </summary>
        protected Vector2f Destination;

        /// <summary>
        /// Цвет квадрата.
        /// </summary>
        protected Color Color;

        /// <summary>
        /// Глобальный игровой бог.
        /// </summary>
        protected Random Random;

        /// <summary>
        /// Минимальная скорость квадрата.
        /// </summary>
        protected int MinSpeed;

        /// <summary>
        /// Максимальная скорость квадрата.
        /// </summary>
        protected int MaxSpeed;

        /// <summary>
        /// Тип квадрата.
        /// </summary>
        protected SquareType Type;

        public Square(Random random, SquareType type, int minSpeed, int maxSpeed, IntRect field)
        {
            Color = Color.White;
            Size = 100;
            Random = random;
            MinSpeed = minSpeed;
            MaxSpeed = maxSpeed;
            Position = new Vector2f(Random.Next(field.Width) + field.Left, Random.Next(field.Height) + field.Top);
            NewDestination(field);
            Type = type;
        }

        /// <summary>
        /// Отрисовка квардрата на экране.
        /// </summary>
        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            switch (Type)
            {
                case SquareType.Square:
                    RectangleShape rectangle = new(new Vector2f(Size, Size))
                        { FillColor = Color, Origin = new Vector2f(Size / 2f, Size / 2f), Position = new Vector2f(Position.X, Position.Y) };
                    rectangle.Draw(target, states);
                    break;
                case SquareType.Circle:
                    CircleShape circle = new CircleShape(Size / 2f)
                        { FillColor = Color, Origin = new Vector2f(Size / 2f, Size / 2f), Position = new Vector2f(Position.X, Position.Y) };
                    circle.Draw(target, states);
                    break;
                case SquareType.Triangle:
                    ConvexShape triangle = new ConvexShape(3)
                        { FillColor = Color, Origin = new Vector2f(Size / 2f, (Size / 2f) / TriangleFactorCos), Position = new Vector2f(Position.X, Position.Y) };
                    triangle.SetPoint(0, new Vector2f(Size / 2f, 0));
                    triangle.SetPoint(1, new Vector2f(0, Size * TriangleFactorCos));
                    triangle.SetPoint(2, new Vector2f(Size, Size * TriangleFactorCos));
                    triangle.Draw(target, states);
                    break;
            }
        }

        /// <summary>
        /// Проверка, попадает ли щелчок мышью по квадрату.
        /// </summary>
        public virtual bool HitTest(Vector2i point)
        {
            bool hit = false;
            Vector2f test;
            switch (Type)
            {
                case SquareType.Square:
                    //проверить, находится ли точка внутри квадрата
                    hit = point.X >= Position.X - Size / 2f && point.X < Position.X + Size / 2f && point.Y >= Position.Y - Size / 2f && point.Y < Position.Y + Size / 2f;
                    break;
                case SquareType.Circle:
                    //проверить, находится ли точка внутри круга
                    test = Position.Sub(point);
                    hit = test.X * test.X + test.Y * test.Y <= Size * Size / 4f;
                    break;
                case SquareType.Triangle:
                    //проверить, находится ли точка внутри треугольника
                    Vector2f ab = new Vector2f(-Size / 2f, Size * TriangleFactorCos);
                    Vector2f bc = new Vector2f(Size, 0);
                    Vector2f ca = new Vector2f(-Size / 2f, -Size * TriangleFactorCos);
                    Vector2f ap = new Vector2f(point.X - Position.X, point.Y - (Position.Y - (Size / 2f) / TriangleFactorCos));
                    Vector2f bp = new Vector2f(point.X - (Position.X - Size / 2f), point.Y - (Position.Y + (Size / 2f) * TriangleFactorSin / TriangleFactorCos));
                    Vector2f cp = new Vector2f(point.X - (Position.X + Size / 2f), point.Y - (Position.Y + (Size / 2f) * TriangleFactorSin / TriangleFactorCos));
                    float abap = ab.VMul(ap);
                    float bcbp = bc.VMul(bp);
                    float cacp = ca.VMul(cp);
                    hit = abap is < 1f and > -1f || bcbp is < 1f and > -1f || cacp is < 1f and > -1f || (abap < 0 && bcbp < 0 && cacp < 0) || (abap > 0 && bcbp > 0 && cacp > 0);
                    break;
            }

            return hit;
        }

        /// <summary>
        /// Перемещение квадрата.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="deltaTime"></param>
        /// <returns></returns>
        public virtual void Move(IntRect field, float deltaTime)
        {
            //увеличить позицию квардата на величину приращения
            Position = Position.Add(Increment.Mul(deltaTime));
            //проверить, достигнута ли точка назначения
            if ((Increment.X is < 1f and > -1f && ((Increment.Y >= 0 && Position.Y >= Destination.Y) || (Increment.Y < 0 && Position.Y < Destination.Y))) || //если угол близок к прямому, игнорировать ось, приращение которой близко к нулю
                (Increment.Y is < 1f and > -1f && ((Increment.X >= 0 && Position.X >= Destination.X) || (Increment.X < 0 && Position.Y < Destination.X))) || //если угол близок к прямому, игнорировать ось, приращение которой близко к нулю
                (((Increment.X >= 0 && Position.X >= Destination.X) || (Increment.X < 0 && Position.X < Destination.X)) && //стандартная проверка
                ((Increment.Y >= 0 && Position.Y >= Destination.Y) || (Increment.Y < 0 && Position.Y < Destination.Y))))
            {
                Position = Destination;
                NewDestination(field);
            }
        }

        /// <summary>
        /// Рассчитать новую точку назначения.
        /// </summary>
        internal void NewDestination(IntRect field)
        {
            //рассчитать координаты точки назначения
            Vector2f test;
            while (true)
            {
                Vector2f destination = new(Random.Next(field.Width) + field.Left, Random.Next(field.Height) + field.Top);
                //рассчитать вектор движения из координат (0;0)
                test = destination.Sub(Position);
                //проверить длину вектора
                if(test.Length() >= MaxSpeed / 2f) 
                {
                    Destination = destination;
                    break;
                }
            }

            //рассчитать угол между вектором и осью 0X
            float angle;
            if (test.X == 0 && test.Y > 0) 
                angle = Pi12;
            else if (test.X == 0 && test.Y < 0) 
                angle = Pi32;
            else if (test.Y == 0 && test.X > 0) 
                angle = 0;
            else if (test.Y == 0 && test.X < 0) 
                angle = Pi;
            else if (test.Y > 0) 
                angle = (float)(Pi12 - Math.Asin(test.X / Math.Sqrt(test.X * test.X + test.Y * test.Y)));
            else 
                angle = (float)(Pi32 + Math.Asin(test.X / Math.Sqrt(test.X * test.X + test.Y * test.Y)));

            //рассчитать скорость
            int speed = MinSpeed == MaxSpeed ? MinSpeed : Random.Next(MinSpeed, MaxSpeed);

            //рассчитать приращение
            Increment.X = (float)(speed * Math.Cos(angle));
            Increment.Y = (float)(speed * Math.Sin(angle));
        }
    }
}
