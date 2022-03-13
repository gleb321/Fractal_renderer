using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fractal {
    /// <summary>
    /// Класс для отрисовки кривой Коха
    /// </summary>
    class Koh: BaseFractal {
        private Point _leftPoint;
        private Point _rightPoint;

        /// <summary>
        /// Инициализирует объект класса Koh
        /// </summary>
        /// <param name="recursionDepth">Глубина рекурсии</param>
        /// <param name="canvas">Холст</param>
        public Koh(int recursionDepth, Canvas canvas) : base(recursionDepth, canvas) {
            _leftPoint = new Point(0, canvas.ActualHeight - 20);
            _rightPoint = new Point(canvas.ActualWidth, canvas.ActualHeight - 20);
        }

        /// <summary>
        /// Базовый метод для отрисовки фрактала
        /// </summary>
        public override void DrawFractal() {
            try {
                DrawKohLine(_recursionDepth, _leftPoint, _rightPoint);
            } catch {
                MessageBox.Show("oops, something go wrong");
            }
        }

        /// <summary>
        /// Метод для отрисовки кривой Коха
        /// </summary>
        /// <param name="recursionDepth">Глубина рекурсии</param>
        /// <param name="leftPoint">Левая точка отрезка</param>
        /// <param name="rightPoint">Правая точка отрезка</param>
        private void DrawKohLine(double recursionDepth, Point leftPoint, Point rightPoint) {
            Point midPoint = new Point((rightPoint.X + leftPoint.X) / 2 + (rightPoint.Y - leftPoint.Y) / 2 / Math.Sqrt(3),
                (rightPoint.Y + leftPoint.Y) / 2 - (rightPoint.X - leftPoint.X) / 2 / Math.Sqrt(3));
            Point subLeftPoint = new Point((2 * leftPoint.X + rightPoint.X) / 3, (2 * leftPoint.Y + rightPoint.Y) / 3);
            Point subRightPoint = new Point((leftPoint.X + 2 * rightPoint.X) / 3, (leftPoint.Y + 2 * rightPoint.Y) / 3);

            Line leftLine = new Line();
            (leftLine.X1, leftLine.Y1, leftLine.X2, leftLine.Y2) = (subLeftPoint.X, subLeftPoint.Y, midPoint.X, midPoint.Y);
            leftLine.Stroke = Brushes.Purple;
            Line rightLine = new Line();
            (rightLine.X1, rightLine.Y1, rightLine.X2, rightLine.Y2) = (midPoint.X, midPoint.Y, subRightPoint.X, subRightPoint.Y);
            rightLine.Stroke = Brushes.Purple;
            Line subLeftLine = new Line();
            (subLeftLine.X1, subLeftLine.Y1, subLeftLine.X2, subLeftLine.Y2) = (leftPoint.X, leftPoint.Y, subLeftPoint.X, subLeftPoint.Y);
            subLeftLine.Stroke = Brushes.Purple;
            Line subRightLine = new Line();
            (subRightLine.X1, subRightLine.Y1, subRightLine.X2, subRightLine.Y2) = (subRightPoint.X, subRightPoint.Y, rightPoint.X, rightPoint.Y);
            subRightLine.Stroke = Brushes.Purple;

            if (recursionDepth > 0) {
                DrawKohLine(recursionDepth - 1, leftPoint, subLeftPoint);
                DrawKohLine(recursionDepth - 1, subLeftPoint, midPoint);
                DrawKohLine(recursionDepth - 1, midPoint, subRightPoint);
                DrawKohLine(recursionDepth - 1, subRightPoint, rightPoint);
            } else {
                _canvas.Children.Add(subLeftLine);
                _canvas.Children.Add(leftLine);
                _canvas.Children.Add(rightLine);
                _canvas.Children.Add(subRightLine);
            }
            return;
        }

        /// <summary>
        /// Обработчик, срабатывающий при изменении размеров холста
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Аргументы</param>
        public override void CanvasSizeChanged(object sender, EventArgs e) {
            _canvas.Children.Clear();
            _leftPoint = new Point(0, _canvas.ActualHeight - 20);
            _rightPoint = new Point(_canvas.ActualWidth, _canvas.ActualHeight - 20);
        }
    }
}
