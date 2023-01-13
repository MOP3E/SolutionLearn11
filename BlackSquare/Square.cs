using System;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;
using SFML.System;
using System.Numerics;

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
        /// Пи * 2
        /// </summary>
        private const float Pi2 = (float)(Math.PI * 2);

        /// <summary>
        /// Пи * 3
        /// </summary>
        private const float Pi3 = (float)(Math.PI * 3);

        /// <summary>
        /// Размер квадрата.
        /// </summary>
        protected int _size;

        /// <summary>
        /// Положение центра квадрата на экране.
        /// </summary>
        protected Vector2f _position;

        /// <summary>
        /// Приращение координат квадрата за одну секунду.
        /// </summary>
        protected Vector2f _increment;

        /// <summary>
        /// Точка назначения квадрата.
        /// </summary>
        protected Vector2f _destination;

        /// <summary>
        /// Цвет квадрата.
        /// </summary>
        protected Color _color = Color.White;

        /// <summary>
        /// Глобальный игровой бог.
        /// </summary>
        protected Random _random;

        /// <summary>
        /// Минимальная скорость квадрата.
        /// </summary>
        protected int _minSpeed;

        /// <summary>
        /// Максимальная скорость квадрата.
        /// </summary>
        protected int _maxSpeed;

        public Square(Random random, int minSpeed, int maxSpeed)
        {
            _random = random;
            _minSpeed = minSpeed;
            _maxSpeed = maxSpeed;
        }

        /// <summary>
        /// Отрисовка квардрата на экране.
        /// </summary>
        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            RectangleShape shape = new(new Vector2f(_size, _size))
                { FillColor = _color, Origin = new Vector2f(_size / 2f, _size / 2f), Position = new Vector2f(_position.X, _position.Y) };
            shape.Draw(target, states);
        }

        /// <summary>
        /// Проверка, попадает ли щелчок мышью по квадрату.
        /// </summary>
        public bool HitTest(Vector2i point)
        {
            return point.X >= _position.X - _size / 2f && point.X < _position.X + _size / 2f && point.Y >= _position.Y - _size / 2f && point.Y < _position.Y + _size / 2f;
        }

        /// <summary>
        /// Перемещение квадрата.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="deltaTime"></param>
        /// <returns></returns>
        public void Move(IntRect field, float deltaTime)
        {
            _position = _position.Add(_increment.Mul(deltaTime));
            if (_position.GtEq(_destination))
            {
                _position = _destination;
                NewDestination(field);
            }
        }

        /// <summary>
        /// Рассчитать новую точку назначения.
        /// </summary>
        private void NewDestination(IntRect field)
        {
            //рассчитать координаты точки назначения
            Vector2f test;
            while (true)
            {
                Vector2f destination = new(_random.Next(field.Width) + field.Left, _random.Next(field.Height) + field.Top);
                test = _destination.Sub(destination);
                if (Math.Abs(test.X) > 10 && Math.Abs(test.Y) > 10)
                {
                    _destination = destination;
                    break;
                }
            }
            
            //рассчитать вектор движения из координат (0;0)
            test = _destination.Sub(_position);

            //рассчитать угол между вектором и осью 0X
            float angle;
            if (test.X == 0 && test.Y > 0) angle = Pi12;
            else if (test.X == 0 && test.Y < 0) angle = Pi32;
            else if (test.Y == 0 && test.X > 0) angle = 0;
            else if (test.Y == 0 && test.X < 0) angle = Pi;
            else if (test.Y > 0) angle = (float)(Pi12 - test.X / Math.Sqrt(test.X * test.X + test.Y * test.Y));
            else angle = (float)(Pi32 + test.X / Math.Sqrt(test.X * test.X + test.Y * test.Y));

            //рассчитать скорость
            int speed = _minSpeed == _maxSpeed ? _minSpeed : _random.Next(_minSpeed, _maxSpeed);

            //рассчитать приращение
            _increment.X = (float)(speed * Math.Cos(angle));
            _increment.Y = (float)(speed * Math.Sin(angle));
        }
    }
}
