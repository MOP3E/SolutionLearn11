using SFML.Graphics;
using SFML.System;

namespace BlackSquare
{
    internal class SquareBlack : Square
    {
        /// <summary>
        /// Максимальный размер квадрата.
        /// </summary>
        private const int MaxSize = 150;

        /// <summary>
        /// Минимальный размер квадрата.
        /// </summary>
        private const int MinSize = 50;

        /// <summary>
        /// Шаг изменения размера квадрата.
        /// </summary>
        private const int Step = 20;

        /// <summary>
        /// Квадрат мёртв.
        /// </summary>
        private bool _dead;

        public SquareBlack(Random random, int minSpeed, int maxSpeed, IntRect field) : base(random, minSpeed, maxSpeed, field)
        {
            Color = Color.Black;
            //Размеры квадрата - от 150х150 до 50х50. 150 - 130 - 110 - 90 - 70 - 50
            Size = MaxSize;
            _dead = false;
        }

        /// <summary>
        /// Проверка попадания в чёрный квадрат. Возвращается истина если квадрат уничтожен.
        /// </summary>
        public override bool HitTest(Vector2i point)
        {
            if (base.HitTest(point))
            {
                Size -= Step;
                if (Size < MinSize)
                {
                    //установить флаг смерти квадрата и вернуть истину
                    _dead = true;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Перемещение чёрного квадрата.
        /// </summary>
        public override void Move(IntRect field, float deltaTime)
        {
            if (_dead)
            {
                //квадрат умер - да здравствует квадрат!
                //обнулить размер квадрата
                Size = MaxSize;
                //переместить квадрат в случайное место
                Position = new Vector2f(Random.Next(field.Width) + field.Left, Random.Next(field.Height) + field.Top);
                //задать новую точку назначения квадрата
                NewDestination(field);
                //сбросить флаг смерти квадрата.
                _dead = false;
                return;
            }

            base.Move(field, deltaTime);
        }
    }
}
