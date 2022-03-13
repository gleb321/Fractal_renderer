using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace Fractal {
    /// <summary>
    /// Класс для отрисовки ковра Серпинского
    /// </summary>
    class Carpet: BaseFractal {
        private Polygon _polygon;
        private double _sideLength;

        /// <summary>
        /// Инициализирует объект класса Capret
        /// </summary>
        /// <param name="recursionDepth">Глубина рекурсии</param>
        /// <param name="canvas">Холст для отрисовки</param>
        /// <param name="sideLength">Длина стороны квадрата</param>
        public Carpet(int recursionDepth, Canvas canvas, double sideLength) :base(recursionDepth, canvas) {
            Point centerPoint = new Point(canvas.ActualWidth / 2, canvas.ActualHeight / 2);
            Point pointLU = new Point(centerPoint.X - sideLength / 2, centerPoint.Y - sideLength / 2);
            Point pointRU = new Point(centerPoint.X + sideLength / 2, centerPoint.Y - sideLength / 2);
            Point pointLD = new Point(centerPoint.X - sideLength / 2, centerPoint.Y + sideLength / 2);
            Point pointRD = new Point(centerPoint.X + sideLength / 2, centerPoint.Y + sideLength / 2);
            _polygon = new Polygon();
            _polygon.Points = new PointCollection(new Point[] { pointRD, pointLD, pointLU, pointRU });
            _sideLength = sideLength;
        }

        /// <summary>
        /// Базовый метод для отрисовки фрактала
        /// </summary>
        public override void DrawFractal() {
            try {
                DrawCarpet(_recursionDepth, _polygon);
            } catch {
                MessageBox.Show("oops, something go wrong");
            }
        }

        /// <summary>
        /// Метод для отрисовки ковра Серпинского
        /// </summary>
        /// <param name="recursionDepth">Глубина рекурсии</param>
        /// <param name="polygon">Изначальный квадрат</param>
        private void DrawCarpet(int recursionDepth, Polygon polygon) {
            polygon.Stroke = Brushes.DarkSeaGreen;
            polygon.Fill = Brushes.DarkSeaGreen;
            _canvas.Children.Add(polygon);
            Point pointLU, pointRU, pointLD, pointRD;
            (pointRD, pointLD, pointLU, pointRU) = (polygon.Points[0], polygon.Points[1], polygon.Points[2], polygon.Points[3]);
           
            Point[] points = new Point[16];
            double deltaX = (pointRU.X - pointLU.X) / 3;
            double deltaY = (pointLD.Y - pointLU.Y) / 3;
            (points[0], points[1], points[2], points[3]) = (pointLU, new Point(pointLU.X + deltaX, pointLU.Y),
                new Point(pointLU.X + 2 * deltaX, pointLU.Y), pointRU);

            for (int i = 4; i < points.Length; ++i) {
                points[i] = new Point(points[i - 4].X, points[i - 4].Y + deltaY);
            }
            Polygon square = new Polygon();
            square.Points = new PointCollection(new Point[] { points[10], points[9], points[5], points[6] });
            square.Stroke = Brushes.White;
            square.Fill = Brushes.White;
            _canvas.Children.Add(square);

            if (recursionDepth > 0) {
                for (int i = 0; i < 3; ++i) {
                    for (int j = 0; j < 3; ++j) {
                        if (!(i == j && i == 1)) {
                            Polygon subSquare = new Polygon();
                            subSquare.Points = new PointCollection(new Point[] { points[5 + 4 * i + j], points[4 + 4 * i + j], points[0 + 4 * i + j], points[1 + 4 * i + j] });
                            DrawCarpet(recursionDepth - 1, subSquare);
                        }
                    }
                }
            }

            return;
        }

        /// <summary>
        /// Обработчик, срабатывающий при изменении значения слайдера, отвечающего за длину стороны
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Аргументы</param>
        public override void LengthChanged(object sender, EventArgs e) {
            SideLength = ((Slider)sender).Value;
        }

        /// <summary>
        /// Свойство для работы с длиной стороны
        /// </summary>
        public double SideLength {
            get => _sideLength;
            set {
                if (value >= 50 && value <= 500) {
                    _sideLength = value;
                } else {
                    MessageBox.Show("Impossible value of square side length");
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
            Point centerPoint = new Point(_canvas.ActualWidth / 2, _canvas.ActualHeight / 2);
            Point pointLU = new Point(centerPoint.X - _sideLength / 2, centerPoint.Y - _sideLength / 2);
            Point pointRU = new Point(centerPoint.X + _sideLength / 2, centerPoint.Y - _sideLength / 2);
            Point pointLD = new Point(centerPoint.X - _sideLength / 2, centerPoint.Y + _sideLength / 2);
            Point pointRD = new Point(centerPoint.X + _sideLength / 2, centerPoint.Y + _sideLength / 2);
            _polygon = new Polygon();
            _polygon.Points = new PointCollection(new Point[] { pointRD, pointLD, pointLU, pointRU });
        }
    }
}
