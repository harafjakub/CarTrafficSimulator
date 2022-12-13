using System;
using System.Collections;
using System.Collections.Generic;
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
            SpawnCarBottom();
            SpawnCarTop();
            SpawnTrain();
        }
        public void SpawnCarBottom()
        {
            Random random = new Random();
            Car car = new Car(random.Next(1,20), 1, "bottom");
            CanvasMain.Children.Add(car.VehicleShape);
            carsListBottom.Add(car);
            carsQueueBottom.Enqueue(car);
            Canvas.SetBottom(carsListBottom[car.ListPosition].VehicleShape, 94);
            Canvas.SetRight(carsListBottom[car.ListPosition].VehicleShape, 0);
            Thread thread = new Thread(MoveCarBottom);
            thread.Start();
        }
        public void MoveCarBottom()
        {
            
            for (int i = 0; i < 30; i++)
            {
                this.Dispatcher.Invoke(() =>
                {
                    carsListBottom[0].VehicleShape.Width = i;
                });
                Thread.Sleep(carsListBottom[0].Speed);
            }
            for (int i = 0; i < 500; i++)
            {
                this.Dispatcher.Invoke(() =>
                {
                    Canvas.SetRight(carsListBottom[0].VehicleShape, 0 + i);
                });
                Thread.Sleep(carsListBottom[0].Speed);
            }
            
        }
        public void SpawnCarTop()
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
        public void MoveCarTop()
        {
            for (int i = 0; i < 30; i++)
            {
                this.Dispatcher.Invoke(() =>
                {
                    carsListTop[0].VehicleShape.Width = i;
                });
                Thread.Sleep(carsListTop[0].Speed);
            }

            // Ruch prosto - w prawo
            for (int i = 0; i < 530; i++)
            {
                this.Dispatcher.Invoke(() =>
                {
                    Canvas.SetLeft(carsListTop[0].VehicleShape, 0 + i);
                });
                Thread.Sleep(carsListTop[0].Speed);
            }

            // Zakret po wewnetrznej w prawo
            for (int i = 0; i < 34; i++)
            {
                this.Dispatcher.Invoke(() => { 
                    Canvas.SetLeft(carsListTop[0].VehicleShape, 530 + i); // 34
                    Canvas.SetTop(carsListTop[0].VehicleShape, 175 + i / 2.8);  // 12
                });
                Thread.Sleep(carsListTop[0].Speed);
            }
            for (int i = 0; i < 29; i++)
            {
                this.Dispatcher.Invoke(() => {
                    Canvas.SetLeft(carsListTop[0].VehicleShape, 564 + i / 1.3); // 22
                    Canvas.SetTop(carsListTop[0].VehicleShape, 187 + i); // 29
                });
                Thread.Sleep(carsListTop[0].Speed);
            }
            for (int i = 0; i < 36; i++)
            {
                this.Dispatcher.Invoke(() => {
                    Canvas.SetTop(carsListTop[0].VehicleShape, 216 + i); // 36
                });
                Thread.Sleep(carsListTop[0].Speed);
            }
            for (int i = 0; i < 29; i++)
            {
                this.Dispatcher.Invoke(() => {
                    Canvas.SetLeft(carsListTop[0].VehicleShape, 586 - i / 1.3); // -22
                    Canvas.SetTop(carsListTop[0].VehicleShape, 252 + i); // 29
                });
                Thread.Sleep(carsListTop[0].Speed);
            }
            for (int i = 0; i < 34; i++)
            {
                this.Dispatcher.Invoke(() => {
                    Canvas.SetLeft(carsListTop[0].VehicleShape, 564 - i); // -34
                    Canvas.SetTop(carsListTop[0].VehicleShape, 281 + i/2.8); // 12
                });
                Thread.Sleep(carsListTop[0].Speed);
            }

            // Ruch prosto - w lewo
            for (int i = 0; i < 380; i++)
            {
                this.Dispatcher.Invoke(() =>
                {
                    Canvas.SetTop(carsListTop[0].VehicleShape, 293);
                    Canvas.SetLeft(carsListTop[0].VehicleShape, 530 - i);
                });
                Thread.Sleep(carsListTop[0].Speed);
            }
        }

        public void SpawnTrain()
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
        public void MoveTrain()
        {
            this.Dispatcher.Invoke(() =>
            {
                trainList[0].VehicleShape.Width = 0;
            });
            Thread.Sleep(5000);
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
    }
}
