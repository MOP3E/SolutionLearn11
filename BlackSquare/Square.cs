using System;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;
using SFML.System;
using System.Numerics;

namespace BlackSquare
{
    /// <summary>
    /// Квадрат.
    /// </summary>
    internal class Square
    {
        /// <summary>
        /// Размер квадрата.
        /// </summary>
        protected int _size;

        /// <summary>
        /// Положение центра квадрата на экране.
        /// </summary>
        protected Vector2 _position;

        /// <summary>
        /// Приращение координат квадрата за одну секунду.
        /// </summary>
        protected Vector2 _increment;

        /// <summary>
        /// Точка назначения квадрата.
        /// </summary>
        protected Vector2 _destination;

        /// <summary>
        /// Цвет квадрата.
        /// </summary>
        protected Color _color = Color.White;

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
    }
}
