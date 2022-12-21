using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;


namespace ProjektWatki
{
    public partial class MainWindow : Window
    {
        #region Fields
        public static SemaphoreSlim listAccess = new SemaphoreSlim(1,1);

        public static List<Vehicle> carsListTop = new List<Vehicle>();
        public static Queue <Vehicle> carsQueueTop = new Queue <Vehicle>();

        public static List<Vehicle> carsListBottom = new List<Vehicle>();
        public static Queue <Vehicle> carsQueueBottom = new Queue <Vehicle>();
       
        public static List<Vehicle> trainList = new List<Vehicle>();
        public static Random random = new Random();
        #endregion
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            OnLoad();
        }
        #endregion
        #region Methods
        private void OnLoad()
        {
            Thread thread_cts = new Thread(CarTopSpawner);
            Thread thread_cbs = new Thread(CarBottomSpawner);
            Thread thread_ts = new Thread(TrainSpawner);
            Thread thread_tft = new Thread(TrafficFromTop);
            Thread thread_tfb = new Thread(TrafficFromBotton);
            Thread thread_rt = new Thread(RailwayTraffic);
            thread_cts.Start();
            thread_cbs.Start();
            thread_ts.Start();
            thread_tft.Start();
            thread_tfb.Start();
            thread_rt.Start();
        }
        #region Vehicles spawner methods
        public void CarBottomSpawner()
        {
            try
            {
                while (true)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        SpawnCarBottom(0.3);
                    });
                    Thread.Sleep(random.Next(3000, 5000));
                }
            }
            catch
            {
            }
        }
        public void CarTopSpawner()
        {
            try
            {
                while (true)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        SpawnCarTop(0.3);
                    });
                    Thread.Sleep(random.Next(3000, 5000));
                }
            }
            catch
            {
            }
        }
        public void TrainSpawner()
        {
            try
            {
                while (true)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        SpawnTrain();
                    });
                    Thread.Sleep(random.Next(10000, 20000));
                }
            }
            catch
            {
            }
        }
        #endregion
        #region Vehicles spawn methods
        public void SpawnCarBottom(double temp)
        {
            try
            {
                Car car = new Car(temp, 0, 94, "bottom");
                car.VehicleShape.Width = 0;
                CanvasMain.Children.Add(car.VehicleShape);
                carsListBottom.Add(car);
                carsQueueBottom.Enqueue(car);
                Canvas.SetBottom(carsListBottom[car.ListPosition].VehicleShape, carsListBottom[car.ListPosition].PositionY);
                Canvas.SetRight(carsListBottom[car.ListPosition].VehicleShape, carsListBottom[car.ListPosition].PositionX);
            }
            catch
            {
            }
        }
        public void SpawnCarTop(double temp)
        {
            try
            {
                Car car = new Car(temp, 0, 175, "top");
                car.VehicleShape.Width = 0;
                CanvasMain.Children.Add(car.VehicleShape);
                carsListTop.Add(car);
                carsQueueTop.Enqueue(car);
                Canvas.SetTop(carsListTop[car.ListPosition].VehicleShape, carsListTop[car.ListPosition].PositionY);
                Canvas.SetLeft(carsListTop[car.ListPosition].VehicleShape, carsListTop[car.ListPosition].PositionX);
            }
            catch
            {
            }
        }
        public void SpawnTrain()
        {
            try
            {
                Train train;
                // tworzenie pociagu ktory pojedzie z lewej strony
                if (random.Next(1, 3) == 1)
                {
                    train = new Train(1, 0, 124, "left");
                    Canvas.SetRight(train.VehicleShape, 0);
                }
                // tworzenie pociagu ktory pojedzie z prawej strony
                else
                {
                    train = new Train(1, 0, 124, "right");
                    Canvas.SetLeft(train.VehicleShape, 0);
                }
                train.VehicleShape.Width = 0;
                CanvasMain.Children.Add(train.VehicleShape);
                trainList.Add(train);
                Canvas.SetBottom(train.VehicleShape, 124);
            }
            catch
            {
            }
        }
        #endregion
        #region Traffic methods
        public void TrafficFromBotton()
        {
            try
            {
                while (true) {
                    List<Vehicle> carsListBottomTemp = new List<Vehicle>(carsListBottom);
                    foreach (Car car in carsListBottomTemp)
                    {
                        // ruch prosto - w lewo
                        if (car.PositionX < 587 && car.PositionX >= 0 && car.PositionY == 94)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                // wjazd spoza horyzontu
                                if(car.VehicleShape.Width < 30)
                                {
                                    Canvas.SetRight(car.VehicleShape, car.PositionX);
                                    Canvas.SetBottom(car.VehicleShape, car.PositionY);
                                    car.VehicleShape.Width += car.Speed;
                                }
                                // ruch prosto
                                else 
                                {
                                    Canvas.SetRight(car.VehicleShape, car.PositionX + car.Speed);
                                    Canvas.SetBottom(car.VehicleShape, car.PositionY);
                                    car.PositionX += car.Speed;
                                }
                                
                            });                               
                        }
                        // zakret po wewnetrznej w prawo
                        else if (car.PositionX >= 587 && car.PositionY >= 94 && Math.Round(car.PositionY) <= 218)
                        {                             
                            this.Dispatcher.Invoke(() =>
                            {
                                (car.PositionX, car.PositionY) = Rotate(587, 94, car.PositionX, car.PositionY, 218, 1 * car.Speed);
                                Canvas.SetRight(car.VehicleShape, car.PositionX);
                                Canvas.SetBottom(car.VehicleShape, car.PositionY);
                            });
                        }
                        // ruch prosto - w prawo
                        else if (car.PositionX > 221 && Math.Round(car.PositionY) == 218)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                Canvas.SetRight(car.VehicleShape, car.PositionX - car.Speed);
                                car.PositionX -= car.Speed;
                            });
                        }
                        // Zakret po zewnetrznej w lewo
                        else if (car.PositionX <= 221 && Math.Round(car.PositionY) >= 218 && car.PositionY <= 396)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                (car.PositionX, car.PositionY) = Rotate(221, 218, car.PositionX, car.PositionY, 396, -1 * car.Speed);
                                Canvas.SetRight(car.VehicleShape, car.PositionX);
                                Canvas.SetBottom(car.VehicleShape, car.PositionY);
                            });
                        }
                        // Ruch prosto - w lewo
                        else if (car.PositionX >= 221 && car.PositionX <= 800 && Math.Round(car.PositionY) == 396)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                Canvas.SetRight(car.VehicleShape, car.PositionX + car.Speed);
                                car.PositionX += car.Speed;
                            });
                        }
                        // Usuwanie obiektu
                        else
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                CanvasMain.Children.Remove(car.VehicleShape);
                                CanvasMain.InvalidateVisual();
                                carsListBottom.Remove(car);
                            });
                        }
                    }
                    Thread.Sleep(1);
                }
            }
            catch
            {
            }
        }
        public void TrafficFromTop()
        {
            try
            {
                while (true)
                {
                    List<Vehicle> carsListTopTemp = new List<Vehicle>(carsListTop);
                    foreach (Car car in carsListTopTemp)
                    {
                        // ruch prosto - w prawo
                        if (car.PositionX < 522 && car.PositionX >= 0 && car.PositionY == 175)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                // wjazd spoza horyzontu
                                if (car.VehicleShape.Width < 30)
                                {
                                    Canvas.SetLeft(car.VehicleShape, car.PositionX);
                                    Canvas.SetTop(car.VehicleShape, car.PositionY);
                                    car.VehicleShape.Width += car.Speed;
                                }
                                // ruch prosto
                                else
                                {
                                    Canvas.SetLeft(car.VehicleShape, car.PositionX + car.Speed);
                                    Canvas.SetTop(car.VehicleShape, car.PositionY);
                                    car.PositionX += car.Speed;
                                }

                            });
                        }
                        // zakret po wewnetrznej w prawo
                        else if (car.PositionX >= 522 && car.PositionY >= 175 && Math.Round(car.PositionY) <= 293)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                (car.PositionX, car.PositionY) = Rotate(522, 175, car.PositionX, car.PositionY, 293, 1 * car.Speed);
                                Canvas.SetLeft(car.VehicleShape, car.PositionX);
                                Canvas.SetTop(car.VehicleShape, car.PositionY);
                            });
                        }
                        // ruch prosto - w lewo
                        else if (car.PositionX > 155 && Math.Round(car.PositionY) == 293)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                Canvas.SetLeft(car.VehicleShape, car.PositionX - car.Speed);
                                car.PositionX -= car.Speed;
                            });
                        }
                        // Zakret po zewnetrznej w lewo
                        else if (car.PositionX <= 155 && Math.Round(car.PositionY) >= 293 && car.PositionY <= 475)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                (car.PositionX, car.PositionY) = Rotate(155, 293, car.PositionX, car.PositionY, 475, -1 * car.Speed);
                                Canvas.SetLeft(car.VehicleShape, car.PositionX);
                                Canvas.SetTop(car.VehicleShape, car.PositionY);
                            });
                        }
                        // Ruch prosto - w prawo
                        else if (car.PositionX >= 155 && car.PositionX <= 800 && Math.Round(car.PositionY) == 475)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                Canvas.SetLeft(car.VehicleShape, car.PositionX + car.Speed);
                                car.PositionX += car.Speed;
                            });
                        }
                        // Usuwanie obiektu
                        else
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                CanvasMain.Children.Remove(car.VehicleShape);
                                CanvasMain.InvalidateVisual();
                                carsListTop.Remove(car);
                            });
                        }
                    }
                    Thread.Sleep(1);
                }
            }
            catch
            {
            }
        }
        public void RailwayTraffic()
        {
            try
            {
                while (true)
                {
                    List<Vehicle> trainListTemp = new List<Vehicle>(trainList);
                    foreach (Train train in trainListTemp)
                    {
                        if (train.PositionX < 800)
                        {
                            // ruch prosto - w prawo
                            if (train.StartingPosition == "left")
                            {
                                this.Dispatcher.Invoke(() =>
                                {
                                    // wjazd spoza horyzontu
                                    if (train.VehicleShape.Width < 150)
                                    {
                                        Canvas.SetRight(train.VehicleShape, train.PositionX);
                                        train.VehicleShape.Width += train.Speed;
                                    }
                                    // ruch prosto
                                    else
                                    {
                                        Canvas.SetRight(train.VehicleShape, train.PositionX + train.Speed);
                                        train.PositionX += train.Speed;
                                    }
                                });
                            }
                            // ruch prosto - w lewo
                            else
                            {
                                this.Dispatcher.Invoke(() =>
                                {
                                    // wjazd spoza horyzontu
                                    if (train.VehicleShape.Width < 150)
                                    {
                                        Canvas.SetLeft(train.VehicleShape, train.PositionX);
                                        train.VehicleShape.Width += train.Speed;
                                    }
                                    // ruch prosto
                                    else
                                    {
                                        Canvas.SetLeft(train.VehicleShape, train.PositionX + train.Speed);
                                        train.PositionX += train.Speed;
                                    }
                                });
                            }
                        }
                        // Usuwanie obiektu
                        else
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                CanvasMain.Children.Remove(train.VehicleShape);
                                CanvasMain.InvalidateVisual();
                                trainList.Remove(train);
                            });
                        }
                    }
                    Thread.Sleep(1);
                }
            } 
            catch
            { 
            }
        }
        #endregion
        #region Additional methods
        public (double, double) Rotate(double x,double startY,double actualX,double actualY, double endY,double degree)
        { // x-CanvasLeft, starty-CanvasTopStart actualx-AktualnyCanvasLeft, actualy-AktualnyCanvasTop, endy-CanvasTopEnd, degree-rotacjeJakzegar-60zakretow-180/60=3(3przekazac)
            double toCalcRad = Math.PI / 180;
            double[,] rotate = new double[2, 2] { { Math.Cos(-toCalcRad * degree), -Math.Sin(-toCalcRad * degree) }, { Math.Sin(-toCalcRad * degree), Math.Cos(-toCalcRad * degree) }};
            double[] carVector = new double[2] {0, 0};
            double[] carVector2 = new double[2] { actualX - x, (((endY + startY) / 2) - actualY) };
            carVector[0] = (rotate[0,0] * carVector2[0]) + (rotate[0, 1] * carVector2[1]);
            carVector[1] = (rotate[1,0] * carVector2[0]) + (rotate[1, 1] * carVector2[1]);
            carVector2[0] = actualX + (carVector[0] - (actualX -x));
            carVector2[1] = ((endY + startY) / 2)- carVector[1];
            return (carVector2[0], carVector2[1]);
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            var response = MessageBox.Show("Do you really want to exit?", "Exiting...", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (response == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Application.Current.Shutdown();
            }
        }
        #endregion
        #endregion
    }
}
