using SFML.Graphics;
using SFML.System;

namespace BlackSquare
{
    /// <summary>
    /// Делегат для активации события супервремени.
    /// </summary>
    delegate void SuperTime();

    internal class SquareBlack : Square
    {
        /// <summary>
        /// Событие супервремени.
        /// </summary>
        public event SuperTime SuperTimeEvent;

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

        /// <summary>
        /// Это суперквадрат.
        /// </summary>
        private bool _super;

        /// <summary>
        /// Суперцвет для суперквадрата.
        /// </summary>
        private Color _superColor = Color.Blue;

        public SquareBlack(Random random, SquareType type, int minSpeed, int maxSpeed, IntRect field) : base(random, type, minSpeed, maxSpeed, field)
        {
            Color = Color.Black;
            //Размеры квадрата - от 150х150 до 50х50. 150 - 130 - 110 - 90 - 70 - 50
            Size = MaxSize;
            _dead = false;
        }

        /// <summary>
        /// Отрисовка квадрата.
        /// </summary>
        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (_super)
            {
                //это суперквадрат - поменять перед отрисовкой цвет на суперцвет
                Color current = Color;
                Color = _superColor;
                base.Draw(target, states);
                //вернуть исходный цвет квадрата
                Color = current;
                return;
            }

            base.Draw(target, states);
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
                    //если это суперквадрат - сгенерировать событие супервремени
                    if(_super)
                    {
                        SuperTimeEvent?.Invoke();
                        _super = false;
                    }
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
                //сбросить флаг смерти квадрата
                _dead = false;
                //с вероятностью 1/10 выпадает суперквадрат, уничтожение которого отключает красные квадраты
                _super = Random.Next(10) == 5;
                return;
            }

            base.Move(field, deltaTime);
        }
    }
}
