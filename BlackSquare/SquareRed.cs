using SFML.Graphics;
using SFML.System;

namespace BlackSquare
{
    internal class SquareRed : Square
    {
        /// <summary>
        /// Минимальная продолжительность жизни квадрата, с.
        /// </summary>
        private const int LifetimeMin = 15;

        /// <summary>
        /// Максимальная продолжительность жизни квадрата, с.
        /// </summary>
        private const int LifetimeMax = 20;

        /// <summary>
        /// Продолжительность жизни квадрата, с.
        /// </summary>
        private float _lifetime;

        /// <summary>
        /// Минимальный размер квадрата.
        /// </summary>
        private const int MinSize = 100;

        /// <summary>
        /// Минимальный размер квадрата.
        /// </summary>
        private const int MaxSize = 200;

        /// <summary>
        /// Время жизни квадрата, с.
        /// </summary>
        private float _life;

        public SquareRed(Random random, SquareType type, int minSpeed, int maxSpeed, IntRect field) : base(random, type, minSpeed, maxSpeed, field)
        {
            Color = Color.Red;
            //Размеры квадрата - от 100х100 до 200х200. Время жизни 15-20 с, размер увеличивается со временем.
            Size = MinSize;
            int lifetime = random.Next(LifetimeMin, LifetimeMax);
            _life = random.Next(lifetime);
            _lifetime = lifetime;
        }

        /// <summary>
        /// Перемещение красного квадрата, пересчёт времени его жизни и размера.
        /// </summary>
        public override void Move(IntRect field, float deltaTime)
        {
            _life += deltaTime;
            if(_life <= _lifetime)
            {
                //квадрат жив
                Size = (int)(MinSize + (float)(MaxSize - MinSize) * _life / _lifetime);
            }
            else
            {
                //квадрат умер - да здравствует квадрат!
                //обнулить размер квадрата и время его жизни
                Size = MinSize;
                _lifetime = Random.Next(LifetimeMin, LifetimeMax);
                _life = 0;
                //переместить квадрат в случайное место
                Position = new Vector2f(Random.Next(field.Width) + field.Left, Random.Next(field.Height) + field.Top);
                //задать новую точку назначения квадрата
                NewDestination(field);
                return;
            }

            base.Move(field, deltaTime);
        }
    }
}
