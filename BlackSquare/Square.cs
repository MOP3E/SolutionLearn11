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
        protected Color _color;

        public Square()
        {

        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {

        }
    }
}
