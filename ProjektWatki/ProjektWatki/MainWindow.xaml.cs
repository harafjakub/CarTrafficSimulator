using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
using static System.Net.Mime.MediaTypeNames;

namespace ProjektWatki
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static SemaphoreSlim listAccess = new SemaphoreSlim(1,1);
        public static Queue <Vehicle> carsQueueTop = new Queue <Vehicle>();
        public static Queue <Vehicle> carsQueueBottom = new Queue <Vehicle>();
        public static Queue <Vehicle> trainQueue = new Queue<Vehicle>();
        public static List <Vehicle> carsListTop = new List<Vehicle>();
        public static List <Vehicle> carsListBottom = new List<Vehicle>();
        public static List <Vehicle> trainList = new List<Vehicle>();
        public MainWindow()
        {
            InitializeComponent();
            OnLoad();
        }
        /*
         * auta powinny byc na liscie aut aby mozna bylo sie odwolac do konkretnego elementu 
         * a w kolejce powinny byc wskazniki na ich pozycje w liscie aby wiedziec kto wyprzedza kogo i aby sprawdzic 
         * czy pierwszy element w kolejce wyjechal poza mape aby go zniszczyc
         * 
         * Sprawdzic czy watek sie wykonal - jesli tak zniszcz go
         * Jak jedna metoda obsluzyc kilka watkow
         */
        private void OnLoad()
        {
            SpawnCarBottom();
            //SpawnCarBottom();
            SpawnCarTop();
            SpawnTrain();
        }
        public void SpawnCarBottom()
        {
            try
            {
                Random random = new Random();
                Car car = new Car(random.Next(1, 20), 1, "bottom");
                CanvasMain.Children.Add(car.VehicleShape);
                carsListBottom.Add(car);
                carsQueueBottom.Enqueue(car);
                Canvas.SetBottom(carsListBottom[car.ListPosition].VehicleShape, 94);
                Canvas.SetRight(carsListBottom[car.ListPosition].VehicleShape, 0);
                Thread thread = new Thread(MoveCarBottom);
                thread.Start();
            }
            catch
            {

            }
        }
        public void SpawnCarTop()
        {
            try
            {
                Random random = new Random();
                Car car = new Car(random.Next(1, 20), 1, "top");
                CanvasMain.Children.Add(car.VehicleShape);
                carsListTop.Add(car);
                carsQueueTop.Enqueue(car);
                Canvas.SetTop(carsListTop[car.ListPosition].VehicleShape, 175);
                Canvas.SetLeft(carsListTop[car.ListPosition].VehicleShape, 0);
                Thread thread = new Thread(MoveCarTop);
                thread.Start();
            }
            catch
            {

            }
        }
        public void SpawnTrain()
        {
            try
            {
                Train train = new Train(1, 1);
                CanvasMain.Children.Add(train.VehicleShape);
                trainList.Add(train);
                trainQueue.Enqueue(train);
                Canvas.SetBottom(train.VehicleShape, 124);
                Canvas.SetRight(train.VehicleShape, 0);
                Thread thread = new Thread(MoveTrain);
                thread.Start();
            }
            catch
            {

            }
        }
        public void MoveCarBottom()
        {
            try
            {
                this.Dispatcher.Invoke(() =>
                {
                    carsListBottom[0].VehicleShape.Width = 0;
                });
                Thread.Sleep(3500);
                // Wychylanie zza horyzontu
                for (int i = 0; i < 30; i++)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        carsListBottom[0].VehicleShape.Width = i;
                    });
                    Thread.Sleep(carsListBottom[0].Speed);
                }
                // Ruch prosto - w lewo
                for (int i = 0; i < 587; i++)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Canvas.SetRight(carsListBottom[0].VehicleShape, 0 + i);
                    });
                    Thread.Sleep(carsListBottom[0].Speed);
                }
                // Zakret po wewnetrznej w prawo
                double[] x = new double[2] { 587, 94 };
                for (int i = 0; i < 179; i++)
                {
                    x = Rotate(587, 94, x[0], x[1], 218, 1);
                    this.Dispatcher.Invoke(() =>
                    {
                        Canvas.SetRight(carsListBottom[0].VehicleShape, x[0]);
                        Canvas.SetBottom(carsListBottom[0].VehicleShape, x[1]);
                    });
                    Thread.Sleep(carsListBottom[0].Speed);
                }
                // Ruch prosto - w prawo
                for (int i = 0; i < 363; i++)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Canvas.SetRight(carsListBottom[0].VehicleShape, 587 - i);
                    });
                    Thread.Sleep(carsListBottom[0].Speed);
                }
                // Zakret po zewwnetrznej w lewo
                x[0] = 224;
                x[1] = 217;
                for (int i = 0; i < 179; i++)
                {
                    x = Rotate(224, 217, x[0], x[1], 396, -1);
                    this.Dispatcher.Invoke(() =>
                    {
                        Canvas.SetRight(carsListBottom[0].VehicleShape, x[0]);
                        Canvas.SetBottom(carsListBottom[0].VehicleShape, x[1]);
                    });
                    Thread.Sleep(carsListBottom[0].Speed);
                }
                // Ruch prosto - w lewo
                for (int i = 0; i < 587; i++)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Canvas.SetRight(carsListBottom[0].VehicleShape, 224 + i);
                    });
                    Thread.Sleep(carsListBottom[0].Speed);
                }
            }
            catch
            {

            }
        }
        public void MoveCarTop()
        {
            try
            {
                this.Dispatcher.Invoke(() =>
                {
                    carsListTop[0].VehicleShape.Width = 0;
                });
                Thread.Sleep(3500);
                // Wychylanie zza horyzontu
                for (int i = 0; i < 30; i++)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        carsListTop[0].VehicleShape.Width = i;
                    });
                    Thread.Sleep(carsListTop[0].Speed);
                }

                // Ruch prosto - w prawo
                for (int i = 0; i < 522; i++)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Canvas.SetLeft(carsListTop[0].VehicleShape, 0 + i);
                    });
                    Thread.Sleep(carsListTop[0].Speed);
                }

                // Zakret po wewnetrznej w prawo
                double[] x = new double[2] { 522, 175 };
                for (int i = 0; i < 179; i++)
                {
                    x = Rotate(522, 175, x[0], x[1], 293, 1);
                    this.Dispatcher.Invoke(() =>
                    {
                        Canvas.SetLeft(carsListTop[0].VehicleShape, x[0]);
                        Canvas.SetTop(carsListTop[0].VehicleShape, x[1]);
                    });
                    Thread.Sleep(carsListTop[0].Speed);
                }

                // Ruch prosto - w lewo
                for (int i = 0; i < 375; i++)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Canvas.SetLeft(carsListTop[0].VehicleShape, 522 - i);
                    });
                    Thread.Sleep(carsListTop[0].Speed);
                }

                // Zakret po zewwnetrznej w lewo
                x[0] = 155;
                x[1] = 293;
                for (int i = 0; i < 179; i++)
                {
                    x = Rotate(155, 293, x[0], x[1], 475, -1);
                    this.Dispatcher.Invoke(() =>
                    {
                        Canvas.SetLeft(carsListTop[0].VehicleShape, x[0]);
                        Canvas.SetTop(carsListTop[0].VehicleShape, x[1]);
                    });
                    Thread.Sleep(carsListTop[0].Speed);
                }

                // Ruch prosto - w prawo
                for (int i = 0; i < 700; i++)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Canvas.SetLeft(carsListTop[0].VehicleShape, 155 + i);
                    });
                    Thread.Sleep(carsListTop[0].Speed);
                }
            }
            catch
            {

            }

        }

        public void MoveTrain()
        {
            try
            {
                this.Dispatcher.Invoke(() =>
                {
                    trainList[0].VehicleShape.Width = 0;
                });
                Thread.Sleep(6000);
                for (int i = 0; i < 150; i++)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        trainList[0].VehicleShape.Width = i;
                    });
                    Thread.Sleep(trainList[0].Speed);
                }
                for (int i = 0; i < 800; i++)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        Canvas.SetRight(trainList[0].VehicleShape, 10 + i);
                    });
                    Thread.Sleep(trainList[0].Speed);
                }
            }
            catch
            {

            }
        }

        public double[] Rotate(double x,double startY,double actualX,double actualY, double endY,double degree)
        { // x-CanvasLeft, starty-CanvasTopStart actualx-AktualnyCanvasLeft, actualy-AktualnyCanvasTop, endy-CanvasTopEnd, degree-rotacjeJakzegar-60zakretow-180/60=3(3przekazac)
            double toCalcRad = Math.PI / 180;
            double[,] rotate = new double[2, 2] { { Math.Cos(-toCalcRad * degree), -Math.Sin(-toCalcRad * degree) }, { Math.Sin(-toCalcRad * degree), Math.Cos(-toCalcRad * degree) }};
            double[] carVector = new double[2] {0, 0};
            double[] carVector2 = new double[2] { actualX - x, (((endY + startY) / 2) - actualY) };
            carVector[0] = (rotate[0,0] * carVector2[0]) + (rotate[0, 1] * carVector2[1]);
            carVector[1] = (rotate[1,0] * carVector2[0]) + (rotate[1, 1] * carVector2[1]);
            carVector2[0] = actualX + (carVector[0] - (actualX -x));
            carVector2[1] = ((endY + startY) / 2)- carVector[1];
            return carVector2;
        }

    }
}
