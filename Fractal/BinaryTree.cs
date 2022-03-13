using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fractal {
    /// <summary>
    /// класс для отрисовки бинарного дерева
    /// </summary>
    class BinaryTree: BaseFractal {
        private double _leftAngleCoef = Math.PI / 4;
        private double _rightAngleCoef = Math.PI / 4;
        private double _lengthCoef;
        private const double _angle = -Math.PI / 2;
        private double _length = 100;
        private Point _startPoint;

        /// <summary>
        /// Инициализирует объект класса BinareeTree
        /// </summary>
        /// <param name="recursionDepth">Глубина рекурсии</param>
        /// <param name="canvas">Холст</param>
        /// <param name="lengthCoef">Коэффициент длины</param>
        public BinaryTree(int recursionDepth, Canvas canvas, double lengthCoef) :base(recursionDepth, canvas) {
            _lengthCoef = lengthCoef;
            _startPoint = new Point(canvas.ActualWidth / 2, canvas.ActualHeight - 30);
        }

        /// <summary>
        /// Базовый метод для отрисовки фракталов
        /// </summary>
        public override void DrawFractal() {
            try {
                DrawBinaryTree(_recursionDepth, _startPoint, _angle, _length);
            } catch {
                MessageBox.Show("oops, something go wrong");
            }
        }

        /// <summary>
        /// Метод для отрисовки бинарного дерева
        /// </summary>
        /// <param name="recursionDepth">Глубина рекурсии</param>
        /// <param name="startPoint">Стартовой точка</param>
        /// <param name="angle">Угл наклона ветви</param>
        /// <param name="length">Длина ветви</param>
        private void DrawBinaryTree(double recursionDepth, Point startPoint, double angle, double length) {
            Point finishPoint = new Point(startPoint.X + length * Math.Cos(angle), startPoint.Y + length * Math.Sin(angle));
            Line line = new Line();
            (line.X1, line.Y1, line.X2, line.Y2) = (startPoint.X, startPoint.Y, finishPoint.X, finishPoint.Y);

            if (_recursionDepth - recursionDepth < 4) {
                line.Stroke = Brushes.Brown;
            } else {
                line.Stroke = Brushes.Pink;
            }

            _canvas.Children.Add(line);
            if (recursionDepth >= 1) {
                DrawBinaryTree(recursionDepth - 1, finishPoint, angle - _leftAngleCoef, length * _lengthCoef);
                DrawBinaryTree(recursionDepth - 1, finishPoint, angle + _rightAngleCoef, length * _lengthCoef);
            }
            return;
        }

        /// <summary>
        /// Обработчик, срабатывающий при изменении значения слайдера, отвечающего за угл наклона левых ветвей
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Аргументы</param>
        public void LeftAngleCoefChanged(object sender, EventArgs e) {
            LeftAngleCoef = ((Slider)sender).Value * Math.PI / 180;
            _canvas.Children.Clear();
            DrawFractal();
        }

        /// <summary>
        /// Обработчик, срабатывающий при изменении значения слайдера, отвечающего за угл наклона правых ветвей
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Аргументы</param>
        public void RightAngleCoefChanged(object sender, EventArgs e) {
            RightAngleCoef = ((Slider)sender).Value * Math.PI / 180;
            _canvas.Children.Clear();
            DrawFractal();
        }

        /// <summary>
        /// Обработчик, срабатывающий при изменении размеров холста
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Аргументы</param>
        public override void CanvasSizeChanged(object sender, EventArgs e) {
            _canvas.Children.Clear();
            _startPoint = new Point(_canvas.ActualWidth / 2, _canvas.ActualHeight - 30);
        }

        /// <summary>
        /// Обработчик, срабатывающий при изменении значения слайдера, отвечающего за коэффициент длины
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Аргументы</param>
        public void LengthCoefChanged(object sender, EventArgs e) {
            _lengthCoef = ((Slider)sender).Value;
            _canvas.Children.Clear();
            DrawFractal();
        }

        /// <summary>
        /// Обработчик, срабатывающий при изменении значения слайдера, отвечающего за длину ветвей
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Аргументы</param>
        public override void LengthChanged(object sender, EventArgs e) {
            Length = ((Slider)sender).Value;
            _canvas.Children.Clear();
            DrawFractal();
        }

        /// <summary>
        /// Свойство для работы с углом наклона левых ветвей
        /// </summary>
        public double LeftAngleCoef {
            get => _leftAngleCoef;
            set {
                if (value >= 0 && value <= Math.PI / 2) {
                    _leftAngleCoef = value;
                } else {
                    MessageBox.Show("Impossible value of angle coefficient");
                }
            }
        }

        /// <summary>
        /// Свойство для работы с углом наклона правых ветвей
        /// </summary>
        public double RightAngleCoef {
            get => _rightAngleCoef;
            set {
                if (value >= 0 && value <= Math.PI / 2) {
                    _rightAngleCoef = value;
                } else {
                    MessageBox.Show("Impossible value of angle coefficient");
                }
            }
        }

        /// <summary>
        /// Свойство для работы с коэффициентом длины ветвей
        /// </summary>
        public double LengthCoef {
            get => _lengthCoef;
            set {
                if (value > 0 && value <= 1) {
                    _lengthCoef = value;
                } else {
                    MessageBox.Show("Impossible value of length coefficient");
                }
            }
        }

        /// <summary>
        /// Свойство для работы с длиной ветвей
        /// </summary>
        public double Length {
            get => _length;
            set {
                if (value >= 50 && value <= 500) {
                    _length = value;
                } else {
                    MessageBox.Show("Impossible value of length");
                }
            }
        }
    }
}
