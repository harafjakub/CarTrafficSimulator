using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjektWatki
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Queue <Rectangle> carsQueueTop = new Queue <Rectangle>();
        public static Queue<Rectangle> carsQueueBottom = new Queue <Rectangle>();
        public static Queue<Rectangle> trainQueue = new Queue<Rectangle>();
        public MainWindow()
        {
            InitializeComponent();
            OnLoad();
        }

        private void OnLoad()
        {
            AddCarTop();
            AddCarBottom();
            AddTrain();
            Thread t1 = new Thread(MoveTopCar);
            Thread t2 = new Thread(MoveBottomCar);
            Thread t3 = new Thread(MoveTrain);
            t1.Start();
            t2.Start();
            t3.Start();
        }
        public void AddCarTop()
        {
            Rectangle carShape = new Rectangle();
            CreateCar(carShape);
            carsQueueTop.Enqueue(carShape);
            Canvas.SetTop(carsQueueTop.Last(), 175);
            Canvas.SetLeft(carsQueueTop.Last(), 0);
        }
        public void AddCarBottom()
        {
            Rectangle carShape = new Rectangle();
            CreateCar(carShape);
            carsQueueBottom.Enqueue(carShape);
            Canvas.SetBottom(carsQueueBottom.Last(), 94);
            Canvas.SetRight(carsQueueBottom.Last(), 0);
        }
        public void AddTrain()
        {
            Rectangle trainShape = new Rectangle();
            CreateTrain(trainShape);
            trainQueue.Enqueue(trainShape);
            Canvas.SetBottom(trainQueue.Last(), 124);
            Canvas.SetRight(trainQueue.Last(), 0);
        }
        public void CreateCar(Rectangle CarShape)
        {
            CarShape.Height = 20;
            CarShape.Width = 30;
            SolidColorBrush blueBrush = new SolidColorBrush();
            blueBrush.Color = Colors.Blue;
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;
            CarShape.StrokeThickness = 4;
            CarShape.Stroke = blackBrush;
            CarShape.Fill = blueBrush;
            CarShape.RadiusX = 10;
            CarShape.RadiusY = 10;
            CanvasMain.Children.Add(CarShape);
        }
        public void CreateTrain(Rectangle TrainShape)
        {  
            TrainShape.Height = 30;
            TrainShape.Width = 150;
            SolidColorBrush blueBrush = new SolidColorBrush();
            blueBrush.Color = Colors.Brown;
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;
            TrainShape.StrokeThickness = 4;
            TrainShape.Stroke = blackBrush;
            TrainShape.Fill = blueBrush;  
            TrainShape.RadiusX = 10;
            TrainShape.RadiusY = 10;
            CanvasMain.Children.Add(TrainShape);
        }
        public void MoveTopCar()
        {
            for (int i = 0; i < 30; i++)
            {
                this.Dispatcher.Invoke(() =>
                {
                    carsQueueTop.Last().Width = i;
                });
                Thread.Sleep(5);
            }

            // Ruch prosto - w prawo
            for (int i = 0; i < 530; i++)
            {
                this.Dispatcher.Invoke(() =>
                {
                    Canvas.SetLeft(carsQueueTop.Last(), 0 + i);
                });
                Thread.Sleep(5);
            }

            // Zakret po wewnetrznej w prawo
            for (int i = 0; i < 34; i++)
            {
                this.Dispatcher.Invoke(() => { 
                    Canvas.SetLeft(carsQueueTop.Last(), 530 + i); // 34
                    Canvas.SetTop(carsQueueTop.Last(), 175 + i/2.8);  // 12
                });
                Thread.Sleep(5);
            }
            for (int i = 0; i < 29; i++)
            {
                this.Dispatcher.Invoke(() => {
                    Canvas.SetLeft(carsQueueTop.Last(), 564 + i / 1.3); // 22
                    Canvas.SetTop(carsQueueTop.Last(), 187 + i); // 29
                });
                Thread.Sleep(5);
            }
            for (int i = 0; i < 36; i++)
            {
                this.Dispatcher.Invoke(() => {
                    Canvas.SetTop(carsQueueTop.Last(), 216 + i); // 36
                });
                Thread.Sleep(5);
            }
            for (int i = 0; i < 29; i++)
            {
                this.Dispatcher.Invoke(() => {
                    Canvas.SetLeft(carsQueueTop.Last(), 586 - i/1.3); // -22
                    Canvas.SetTop(carsQueueTop.Last(), 252 + i); // 29
                });
                Thread.Sleep(5);
            }
            for (int i = 0; i < 34; i++)
            {
                this.Dispatcher.Invoke(() => {
                    Canvas.SetLeft(carsQueueTop.Last(), 564 - i); // -34
                    Canvas.SetTop(carsQueueTop.Last(), 281 + i/2.8); // 12
                });
                Thread.Sleep(5);
            }

            // Ruch prosto - w lewo
            for (int i = 0; i < 380; i++)
            {
                this.Dispatcher.Invoke(() =>
                {
                    Canvas.SetTop(carsQueueTop.Last(), 293);
                    Canvas.SetLeft(carsQueueTop.Last(), 530 - i);
                });
                Thread.Sleep(5);
            }
        }
        public void MoveBottomCar()
        {
            for (int i = 0; i < 30; i++)
            {
                this.Dispatcher.Invoke(() =>
                {
                    carsQueueBottom.Last().Width = i;
                });
                Thread.Sleep(10);
            }
            for (int i = 0; i < 500; i++)
            {
                this.Dispatcher.Invoke(() =>
                {
                    Canvas.SetRight(carsQueueBottom.Last(), 0 + i);
                });
                Thread.Sleep(10);
            }
        }
        public void MoveTrain()
        {
            for (int i=0; i < 150; i++)
            {
                this.Dispatcher.Invoke(() =>
                {
                    trainQueue.Last().Width=i;
                });
                Thread.Sleep(1);
            }
            for (int i = 0; i < 800; i++)
            {
                this.Dispatcher.Invoke(() =>
                {
                    Canvas.SetRight(trainQueue.Last(), 10 + i);
                });
                Thread.Sleep(1);
            }
        }
    }
}
