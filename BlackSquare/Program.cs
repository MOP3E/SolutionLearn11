using System;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;
using SFML.System;

namespace BlackSquare
{
    internal class Program
    {
        /// <summary>
        /// Окно отрисовки.
        /// </summary>
        private static RenderWindow _window;
        
        /// <summary>
        /// Предыдущее состояние клавиши ECS.
        /// </summary>
        private static bool _prevEsc;
        
        /// <summary>
        /// Текущее состояние игры.
        /// </summary>
        private static GameState _state;

        /// <summary>
        /// Параметры игрового поля.
        /// </summary>
        private static IntRect _field;

        /// <summary>
        /// Игровой бог.
        /// </summary>
        private static Random _random;

        /// <summary>
        /// Текст для вывода текста на экран.
        /// </summary>
        private static Text _text;

        static void Main(string[] args)
        {
            //молитва игровому богу
            _random = new Random((int)(DateTime.Now.Ticks & 0xFFFFFFFF));

            //инициализация окна
            _window = new RenderWindow(new VideoMode(800, 600), "Чёрный квадрат");
            _window.SetFramerateLimit(60);
            _window.Closed += _window_Closed;
            //_window.SetMouseCursorVisible(false);
            //_window.SetMouseCursorGrabbed(true);

            //настройка игрового текста
            _text = new Text();
            _text.Font = new Font("comic.ttf");

            //настройка игрового поля
            _field = new IntRect(0, 30, 800, 600);

            //создание часов
            Clock clock = new();
            clock.Restart();

            Square square = new(_random, 200, 200, _field);

            while (_window.IsOpen)
            {
                float deltaTime = clock.ElapsedTime.AsSeconds();
                clock.Restart();

                _window.Clear(Color.Black);

                _window.DispatchEvents();

                //проверка нажатий на кнопки мыши
                MouseState.ButtonsTest();

                //обнаружение нажатия на кнопку ESC
                bool esc = Keyboard.IsKeyPressed(Keyboard.Key.Escape);
                bool escPressed = _prevEsc && _prevEsc != esc;
                _prevEsc = esc;
                
                //быстрый выход в главное меню
                if (_state != GameState.MainMenu)
                {
                    //из любого режима игры по нажатию кнопки ECS всегда возврат в главное меню
                    if (escPressed)
                    {
                        _state = GameState.MainMenu;
                        //нажатие на кнопку перехвачено и обработано
                        escPressed = false;
                    }
                }

                //игровая логика
                square.Move(_field, deltaTime);
                
                //отрисовка игрового экрана
                square.Draw(_window, RenderStates.Default);

                _window.Display();
            }
        }

        /// <summary>
        /// Событие нажатия на кнопку закрытия окна.
        /// </summary>
        private static void _window_Closed(object sender, EventArgs e)
        {
            _window.Close();
        }

        /// <summary>
        /// Вывод на экран текста.
        /// </summary>
        public static void DrawText(string text, uint size, Color color, int x, int y)
        {
            _text.DisplayedString = text;
            _text.CharacterSize = size;
            _text.FillColor = color;
            _text.Position = new Vector2f(x, y);
            _text.Draw(_window, RenderStates.Default);
        }
    }
}