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
        /// Игровой бог.
        /// </summary>
        private static Random _random;


        static void Main(string[] args)
        {
            //молитва игровому богу
            _random = new Random((int)(DateTime.Now.Ticks & 0xFFFFFFFF));

            //инициализация окна
            _window = new RenderWindow(new VideoMode(800, 600), "Чёрный квадрат");
            _window.SetFramerateLimit(60);
            _window.Closed += _window_Closed;
            _window.SetMouseCursorVisible(false);
            _window.SetMouseCursorGrabbed(true);
                        
            //создание часов
            Clock clock = new();
            clock.Restart();

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
                
                //отрисовка игрового экрана

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
    }
}