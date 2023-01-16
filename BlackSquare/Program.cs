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
        /// Список всех квадратов на поле.
        /// </summary>
        private static List<Square> _squares;

        /// <summary>
        /// Список чёрных квадратов на поле.
        /// </summary>
        private static List<SquareBlack> _blacks;

        /// <summary>
        /// Список красных квадратов на поле.
        /// </summary>
        private static List<SquareRed> _reds;
        
        /// <summary>
        /// Текущие очки.
        /// </summary>
        private static int _score;

        /// <summary>
        /// Рекорд.
        /// </summary>
        private static int _record;

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

            //обнулить рекорд
            _record = 0;
            
            //переключить игру в режим главного меню
            _state = GameState.MainMenu;

            //создание часов
            Clock clock = new();
            clock.Restart();

            while (_window.IsOpen)
            {
                float deltaTime = clock.ElapsedTime.AsSeconds();
                clock.Restart();

                _window.Clear(new Color(128, 128, 128));

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
                switch (_state)
                {
                    case GameState.MainMenu:
                        if (MouseState.LeftButtonPressed)
                        {
                            //по нажатию кнопки мыши начать новую игру
                            NewGame();
                        }

                        //выход из программы
                        if(escPressed) 
                            _window.Close();
                        break;
                    case GameState.Game:
                        if (escPressed)
                            _state = GameState.MainMenu;
                        else
                        {
                            //проверить, не нажата ли кнопка мыши
                            if (MouseState.LeftButtonPressed)
                            {
                                //получить позицию мыши и проверить, не попало ли нажатие в квадрат
                                Vector2i point = Mouse.GetPosition(_window);

                                foreach (SquareRed red in _reds)
                                {
                                    if (red.HitTest(point))
                                    {
                                        //игра окончена
                                        if (_score > _record)
                                        {
                                            _record = _score;
                                            _state = GameState.GamoverRecord;
                                        }
                                        else
                                        {
                                            _state = GameState.Gamover;
                                        }
                                        break;
                                    }
                                }
                                
                                //завершение работы ветки если игра окончена
                                if(_state != GameState.Game) break;

                                foreach (SquareBlack black in _blacks)
                                {
                                    //проверить, не убит ли чёрный квадрат
                                    if (black.HitTest(point))
                                    {
                                        _score++;
                                        break;
                                    }
                                }
                            }

                            foreach (Square square in _squares)
                            {
                                square.Move(_field, deltaTime);
                            }
                        }
                        break;
                    case GameState.Gamover:
                    case GameState.GamoverRecord:
                        if (MouseState.LeftButtonPressed || escPressed)
                        {
                            _state = GameState.MainMenu;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                //отрисовка игрового экрана
                Draw();

                _window.Display();
            }
        }

        /// <summary>
        /// Начало новой игры.
        /// </summary>
        private static void NewGame()
        {
            //обнулить очки
            _score = 0;

            //создать чёрные квадраты
            _blacks = new List<SquareBlack>
            {
                new(_random, 180, 250, _field),
                new(_random, 180, 250, _field),
                new(_random, 180, 250, _field),
                new(_random, 180, 250, _field),
                new(_random, 180, 250, _field),
            };

            //создать красные квадраты
            _reds = new List<SquareRed>
            {
                new(_random, 80, 120, _field),
                new(_random, 80, 120, _field),
                new(_random, 80, 120, _field),
            };

            //создать список всех квадратов
            _squares = new List<Square>();
            _squares.AddRange(_blacks);
            _squares.AddRange(_reds);

            _state = GameState.Game;
        }

        /// <summary>
        /// Отрисовка игрового экрана.
        /// </summary>
        private static void Draw()
        {
            switch (_state)
            {
                case GameState.MainMenu:
                    //TODO: нарисовать главное меню
                    DrawText("Чёрный квадрат", 72, Color.Yellow, 0, 0);
                    break;
                case GameState.Game:
                    //TODO: нарисовать очки
                    foreach (Square square in _squares)
                    {
                        square.Draw(_window, RenderStates.Default);
                    }
                    break;
                case GameState.Gamover:
                    //TODO: нарисовать сообщение об окончании игры
                    DrawText("Гамовер", 72, Color.Yellow, 0, 0);
                    break;
                case GameState.GamoverRecord:
                    //TODO: нарисовать сообщение об окончании игры с рекордом
                    DrawText("Гамовер с рекордом", 72, Color.Yellow, 0, 0);
                    break;
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
        private static void DrawText(string text, uint size, Color color, int x, int y)
        {
            _text.DisplayedString = text;
            _text.CharacterSize = size;
            _text.FillColor = color;
            _text.Position = new Vector2f(x, y);
            _text.Draw(_window, RenderStates.Default);
        }
    }
}