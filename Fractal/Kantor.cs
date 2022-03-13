using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fractal {
    /// <summary>
    /// Класс для отрисовки множества кантора
    /// </summary>
    class Kantor : BaseFractal {
        private Line _line;
        private double _deltaY;
        public Kantor(int recursionDepth, Canvas canvas, double deltaY) : base(recursionDepth, canvas) {
            _line = new Line();
            (_line.X1, _line.Y1, _line.X2, _line.Y2) = (20, 20, canvas.ActualWidth - 20, 20);
            _deltaY = deltaY;
        }

        /// <summary>
        /// Базовый метод для отрисовки фракталов
        /// </summary>
        public override void DrawFractal() {
            try {
                DrawCantor(_recursionDepth, _line);
            } catch {
                MessageBox.Show("oops, something go wrong");
            }
        }

        /// <summary>
        /// Метод для отрисовки множества Кантора
        /// </summary>
        /// <param name="recursionDepth">Глубина рекурсии</param>
        /// <param name="line"></param>
        public void DrawCantor(int recursionDepth, Line line) {
            line.StrokeThickness = 15;
            line.Stroke = Brushes.Black;
            _canvas.Children.Add(line);
            double deltaX = (line.X2 - line.X1) / 3;
            Line leftLine = new Line();
            Line rightLine = new Line();
            (leftLine.X1, leftLine.Y1, leftLine.X2, leftLine.Y2) = (line.X1, line.Y1 + _deltaY, line.X1 + deltaX, line.Y1 + _deltaY);
            (rightLine.X1, rightLine.Y1, rightLine.X2, rightLine.Y2) = (line.X2 - deltaX, line.Y1 + _deltaY, line.X2, line.Y1 + _deltaY);
            if (recursionDepth > 0) {
                DrawCantor(recursionDepth - 1, leftLine);
                DrawCantor(recursionDepth - 1, rightLine);
            }

            return;
        }

        /// <summary>
        /// Свойство для работы с deltaY
        /// </summary>
        public double DeltaY {
            get => _deltaY;
            set {
                if (value >= 20 && value <= 100) {
                    _deltaY = value;
                } else {
                    MessageBox.Show("Impossible value of deltaY");
                }
            }
        }

        /// <summary>
        /// Обработчик, срабатывающий при изменении размеров холста
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Аргументы</param>
        public override void CanvasSizeChanged(object sender, EventArgs e) {
            _canvas.Children.Clear();
            (_line.X1, _line.Y1, _line.X2, _line.Y2) = (20, 20, _canvas.ActualWidth - 20, 20);
        }

        /// <summary>
        /// Обработчик, срабатывающий при изменении значения слайдера, отвечающего за deltaY
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Аргументы</param>
        public void DeltaYChanged(object sender, EventArgs e) {
            DeltaY = ((Slider)sender).Value;
            _canvas.Children.Clear();
            DrawFractal();
        }
    }
}
