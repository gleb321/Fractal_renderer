using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fractal {
    
    public partial class MainWindow : Window {
        BaseFractal[] fractals = new BaseFractal[5];
        int index;
        bool typeChanged = false;

        /// <summary>
        /// Инициализирует окно приложения
        /// </summary>
        public MainWindow() {
            InitializeComponent();
            BinaryTree binareeTree = new BinaryTree(10, canvas, 0.7);
            Koh koh = new Koh(6, canvas);
            Carpet carpet = new Carpet(4, canvas, 500);
            Triangle triangle = new Triangle(7, canvas, 400);
            Kantor kantor = new Kantor(10, canvas, 40);
            leftAngleSlider.ValueChanged += binareeTree.LeftAngleCoefChanged;
            rightAngleSlider.ValueChanged += binareeTree.RightAngleCoefChanged;
            lengthSlider.ValueChanged += binareeTree.LengthChanged;
            lengthCoefSlider.ValueChanged += binareeTree.LengthCoefChanged;
            distanceSlider.ValueChanged += kantor.DeltaYChanged;
            fractals = new BaseFractal[]{binareeTree, koh, carpet, triangle, kantor};
            foreach (var fractal in fractals) {
                mainWindow.SizeChanged += fractal.CanvasSizeChanged;
                recursionDepthSlider.ValueChanged += fractal.RecursionDepthChanged;
            }
            recursionDepthSlider.ValueChanged += recursionDepthSlider_ValueChanged;
            rightAngleSlider.Value = leftAngleSlider.Value = 45;
            lengthSlider.Value = 100;
            lengthCoefSlider.Value = 0.7;
            distanceSlider.Value = 40;
            recursionDepthSlider.Value = 10;
            fractalList.SelectedIndex = 0;
        }

        /// <summary>
        /// Обработчик, срабатывающий при нажатии на кнопку Draw
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDraw_Click(object sender, RoutedEventArgs e) {
            canvas?.Children.Clear();
            fractals[index]?.DrawFractal();
        }

        /// <summary>
        /// Обработчик, срабатывающий при изменении типа фрактала
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FractalTypeChanged(object sender, EventArgs e) {
            canvas?.Children.Clear();
            index = ((ComboBox)sender).SelectedIndex;
            if (index == 0) {
                rightAngleInfo.Visibility = rightAngleSlider.Visibility = rightAngleLabel.Visibility = Visibility.Visible;
                leftAngleInfo.Visibility = leftAngleSlider.Visibility = leftAngleLabel.Visibility = Visibility.Visible;
                lengthInfo.Visibility = lengthSlider.Visibility = lengthLabel.Visibility = Visibility.Visible;
                lengthCoefInfo.Visibility = lengthCoefSlider.Visibility = lengthCoefLabel.Visibility = Visibility.Visible;
            } else {
                rightAngleInfo.Visibility = rightAngleSlider.Visibility = rightAngleLabel.Visibility = Visibility.Hidden;
                leftAngleInfo.Visibility = leftAngleSlider.Visibility = leftAngleLabel.Visibility = Visibility.Hidden;
                lengthInfo.Visibility = lengthSlider.Visibility = lengthLabel.Visibility = Visibility.Hidden;
                lengthCoefInfo.Visibility = lengthCoefSlider.Visibility = lengthCoefLabel.Visibility = Visibility.Hidden;
            }

            if (index == 4) {
                distanceInfo.Visibility = distanceSlider.Visibility = distanceLabel.Visibility = Visibility.Visible;
            } else {
                distanceInfo.Visibility = distanceSlider.Visibility = distanceLabel.Visibility = Visibility.Hidden;
            }

            typeChanged = true;
            recursionDepthSlider.Value = recursionDepthSlider.Maximum = fractals[index].maxRecursionDepth;
            recurssionDepthInfo.Content = fractals[index].maxRecursionDepth;
        }

        /// <summary>
        /// Обработчик, срабатывающий при изменении значения слайдера, отвечающиего за глубину рекурсии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void recursionDepthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            if (!typeChanged) {
                canvas?.Children.Clear();
                recurssionDepthInfo.Content = (int)((Slider)sender).Value;
                fractals[index]?.DrawFractal();
            }
            typeChanged = false;
        }

        /// <summary>
        /// Обработчик, срабатывающий при изменении значения слайдера, отвечающиего за угл наклона левых ветвей
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Аругменты</param>
        private void leftAngleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            leftAngleInfo.Content = (int)((Slider)sender).Value;
        }

        /// <summary>
        /// Обработчик, срабатывающий при изменении значения слайдера, отвечающиего за угл наклона правых ветвей
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Аругменты</param>
        private void rightAngleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            rightAngleInfo.Content = (int)((Slider)sender).Value;
        }

        /// <summary>
        /// Обработчик, срабатывающий при изменении значения слайдера, отвечающиего за длину
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Аругменты</param>
        private void lengthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            lengthInfo.Content = (int)((Slider)sender).Value;
        }

        /// <summary>
        /// Обработчик, срабатывающий при изменении значения слайдера, отвечающиего за коэффициент изменения длины
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Аругменты</param>
        private void lengthCoefSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            lengthCoefInfo.Content = ((Slider)sender).Value;
        }

        /// <summary>
        /// Обработчик, срабатывающий при изменении значения слайдера, отвечающиего за длину расстояния между отрезками
        /// </summary>
        /// <param name="sender">Издатель</param>
        /// <param name="e">Аругменты</param>
        private void distanceSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            distanceInfo.Content = ((Slider)sender).Value;
        }
    }
}
