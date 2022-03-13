using System;
using System.Windows;
using System.Windows.Controls;

namespace Fractal {
    /// <summary>
    /// Базовый класс для отрисовки фракталов
    /// </summary>
    public abstract class BaseFractal {
        protected int _recursionDepth = 1;
        protected Canvas _canvas;
        public readonly int maxRecursionDepth;

        /// <summary>
        /// Конструктор базового класса
        /// </summary>
        /// <param name="recursionDepth">Глубина рекурсии</param>
        /// <param name="canvas">Холст</param>
        public BaseFractal(int recursionDepth, Canvas canvas) {
            (_recursionDepth, _canvas) = (recursionDepth, canvas);
            maxRecursionDepth = recursionDepth;
        }

        /// <summary>
        /// Базовый метод для отрисовки фракталов
        /// </summary>
        public virtual void DrawFractal() { }

        /// <summary>
        /// Свойсто для работы с глубиной рекурсии
        /// </summary>
        public virtual int RecurssionDepth {
            get => _recursionDepth;
            set {
                if (value >= 0 && value <= 15) {
                    _recursionDepth = value;
                } else {
                    MessageBox.Show("Impossible depth of recurssion");
                }
            }
        }

        /// <summary>
        /// Обработчик, срабатывающий при изменении размеров холста
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Аргументы</param>
        public virtual void CanvasSizeChanged(object sender, EventArgs e) { }

        /// <summary>
        /// Обработчик, срабатывающий при изменении значения слайдера, отвечающего за длину
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Аргументы</param>
        public virtual void LengthChanged(object sender, EventArgs e) { }

        /// <summary>
        /// Обработчик, срабатывающий при изменении значения слайдера, отвечающего за глубину рекурсии
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Аргументы</param>
        public virtual void RecursionDepthChanged(object sender, EventArgs e) {
            RecurssionDepth = (int)((Slider)sender).Value;
        }
    }
}
