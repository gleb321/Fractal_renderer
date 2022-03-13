using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fractal {
    /// <summary>
    /// Класс для отрисовки Треугольника Серпинского
    /// </summary>
    class Triangle: BaseFractal {
        private Polygon _polygon;
        private double _sideLength;

        /// <summary>
        /// Инициализирует объеут класса Triangle
        /// </summary>
        /// <param name="recursionDepth"></param>
        /// <param name="canvas"></param>
        /// <param name="sideLength"></param>
        public Triangle(int recursionDepth, Canvas canvas, double sideLength) : base(recursionDepth, canvas) {
            Point centerPoint = new Point(canvas.ActualWidth / 2, canvas.ActualHeight / 2);
            Point pointA = new Point(centerPoint.X, centerPoint.Y - sideLength / Math.Sqrt(3));
            Point pointB = new Point(centerPoint.X - sideLength / 2, centerPoint.Y + sideLength / 2 / Math.Sqrt(3));
            Point pointC = new Point(centerPoint.X + sideLength / 2, centerPoint.Y + sideLength / 2 / Math.Sqrt(3));
            _polygon = new Polygon();
            _polygon.Points = new PointCollection(new Point[] { pointA, pointB, pointC});
            _sideLength = sideLength;
        }

        /// <summary>
        /// Базовый метод для отрисовки фрактала
        /// </summary>
        public override void DrawFractal() {
            try {
                DrawTriangle(_recursionDepth, _polygon);
            } catch {
                MessageBox.Show("oops, something go wrong");
            }
        }

        /// <summary>
        /// Метод для отрисовки треугольника Серпинского
        /// </summary>
        /// <param name="recursionDepth"></param>
        /// <param name="polygon"></param>
        public void DrawTriangle(int recursionDepth, Polygon polygon) {
            polygon.Stroke = Brushes.Coral;
            _canvas.Children.Add(polygon);
            Point pointA, pointB, pointC;
            (pointA, pointB, pointC) = (polygon.Points[0], polygon.Points[1], polygon.Points[2]);
            Point pointAB = new Point((pointA.X + pointB.X) / 2, (pointA.Y + pointB.Y) / 2);
            Point pointBC = new Point((pointB.X + pointC.X) / 2, (pointB.Y + pointC.Y) / 2);
            Point pointAC = new Point((pointA.X + pointC.X) / 2, (pointA.Y + pointC.Y) / 2);
            Polygon triangle = new Polygon();
            triangle.Points = new PointCollection(new Point[] { pointAB, pointAC, pointBC });
            triangle.Stroke = Brushes.Coral;
            _canvas.Children.Add(triangle);
            if (recursionDepth > 1) {
                Polygon subTriangle1 = new Polygon();
                subTriangle1.Points = new PointCollection(new Point[] { pointAB, pointAC, pointA });
                DrawTriangle(recursionDepth - 1, subTriangle1);
                Polygon subTriangle2 = new Polygon();
                subTriangle2.Points = new PointCollection(new Point[] { pointAB, pointBC, pointB });
                DrawTriangle(recursionDepth - 1, subTriangle2);
                Polygon subTriangle3 = new Polygon();
                subTriangle3.Points = new PointCollection(new Point[] { pointBC, pointAC, pointC });
                DrawTriangle(recursionDepth - 1, subTriangle3);
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
            Point pointA = new Point(centerPoint.X, centerPoint.Y - _sideLength / Math.Sqrt(3));
            Point pointB = new Point(centerPoint.X - _sideLength / 2, centerPoint.Y + _sideLength / 2 / Math.Sqrt(3));
            Point pointC = new Point(centerPoint.X + _sideLength / 2, centerPoint.Y + _sideLength / 2 / Math.Sqrt(3));
            _polygon = new Polygon();
            _polygon.Points = new PointCollection(new Point[] { pointA, pointB, pointC });
        }
    }
}
