using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;

namespace BlackSquare
{
    internal static class MouseState
    {
        /// <summary>
        /// Предыдущеее состояние левой кнопки мыши.
        /// </summary>
        private static bool _prevLeftButton;

        /// <summary>
        /// Нажата левая кнопка мыши.
        /// </summary>
        public static bool LeftButtonPressed;

        /// <summary>
        /// Отпущена левая кнопка мыши.
        /// </summary>
        public static bool LeftButtonReleased;

        /// <summary>
        /// Предыдущеее состояние средней кнопки мыши.
        /// </summary>
        private static bool _prevMiddleButton;

        /// <summary>
        /// Нажата центральная кнопка мыши.
        /// </summary>
        public static bool MiddleButtonPressed;

        /// <summary>
        /// Отпущена центральная кнопка мыши.
        /// </summary>
        public static bool MiddleButtonReleased;

        /// <summary>
        /// Предыдущеее состояние правой кнопки мыши.
        /// </summary>
        private static bool _prevRightButton;

        /// <summary>
        /// Нажата правя кнопка мыши.
        /// </summary>
        public static bool RightButtonPressed;

        /// <summary>
        /// Отпущена правая кнопка мыши.
        /// </summary>
        public static bool RightButtonReleased;

        /// <summary>
        /// Статический конструктор.
        /// </summary>
        static MouseState()
        {
            ButtonsTest();
        }

        /// <summary>
        /// Функция проверки нажатий на кнопки мыши.
        /// </summary>
        public static void ButtonsTest()
        {
            //левая кнопка мыши
            LeftButtonPressed = false;
            LeftButtonReleased = false;
            bool leftButton = Mouse.IsButtonPressed(Mouse.Button.Left);
            if (leftButton)
            {
                if (!_prevLeftButton)
                {
                    LeftButtonPressed = true;
                }
            }
            else
            {
                if (_prevLeftButton)
                {
                    LeftButtonReleased = true;
                }
            }
            _prevLeftButton = leftButton;

            //средняя кнопка мыши
            MiddleButtonPressed = false;
            MiddleButtonReleased = false;
            bool middleButton = Mouse.IsButtonPressed(Mouse.Button.Middle);
            if (middleButton)
            {
                if (!_prevMiddleButton)
                {
                    MiddleButtonPressed = true;
                }
            }
            else
            {
                if (_prevMiddleButton)
                {
                    MiddleButtonReleased = true;
                }
            }
            _prevMiddleButton = middleButton;

            //правая кнопка мыши
            RightButtonPressed = false;
            RightButtonReleased = false;
            bool rightButton = Mouse.IsButtonPressed(Mouse.Button.Left);
            if (rightButton)
            {
                if (!_prevRightButton)
                {
                    RightButtonPressed = true;
                }
            }
            else
            {
                if (_prevRightButton)
                {
                    RightButtonReleased = true;
                }
            }
            _prevLeftButton = rightButton;
        }
    }
}
