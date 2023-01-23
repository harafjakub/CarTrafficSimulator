using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ProjektWatki
{
    public partial class MainWindow : Window
    {
        #region Fields
        public static bool stop = true;
        public static List<Vehicle> carsListTop = new List<Vehicle>();
        public static List<Vehicle> carsListBottom = new List<Vehicle>();
        public static List<Vehicle> trainList = new List<Vehicle>();
        public static Random random = new Random();
        public static int multyply = 1;      
        #endregion
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            OnLoad();
            IsDebugOrExe();
        }
        #endregion
        #region Methods
        private void OnLoad()
        {
            Thread thread_cts = new Thread(CarTopSpawner); // thread for random time top car spawn
            Thread thread_cbs = new Thread(CarBottomSpawner); // thread for random time bottom car spawn
            Thread thread_ts = new Thread(TrainSpawner); // thread for random time left or right train spawn
            Thread thread_tft = new Thread(TrafficFromTop); // thread for movement (for top spawn car)
            Thread thread_tfb = new Thread(TrafficFromBotton); // thread for movement (for bottom spawn car)
            Thread thread_rt = new Thread(RailwayTraffic); // thread for movement (for all trains)
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
                        SpawnCarBottom();
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
                        SpawnCarTop();
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
                    Thread.Sleep(random.Next(8000, 12000));
                }
            }
            catch
            {
            }
        }
        #endregion
        #region Vehicles spawn methods
        public void SpawnCarBottom()
        {
            try
            {
                Car car = new Car((random.NextDouble() * (0.45*multyply - 0.15 * multyply)) + 0.15 * multyply, 0, 94, (random.NextDouble() * (80 - 60)) + 60, "bottom");
                car.VehicleShape.Width = 0;
                CanvasMain.Children.Add(car.VehicleShape);
                carsListBottom.Add(car);
                Canvas.SetBottom(carsListBottom[car.ListPosition].VehicleShape, carsListBottom[car.ListPosition].PositionY);
                Canvas.SetRight(carsListBottom[car.ListPosition].VehicleShape, carsListBottom[car.ListPosition].PositionX);
            }
            catch
            {
            }
        }
        public void SpawnCarTop()
        {
            try
            {
                Car car = new Car((random.NextDouble() * (0.45 * multyply - 0.15 * multyply)) + 0.15 * multyply, 0, 175, (random.NextDouble() * (80 - 60)) + 60, "top");
                car.VehicleShape.Width = 0;
                CanvasMain.Children.Add(car.VehicleShape);
                carsListTop.Add(car);
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
                // creating a train that will go from the left
                if (random.Next(1, 3) == 1)
                {
                    train = new Train(0.8 * multyply, 0, 124, "right");
                    Canvas.SetRight(train.VehicleShape, 0);
                }
                // creating a train that will go from the right
                else
                {
                    train = new Train(0.8 * multyply, 0, 124, "left");
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
                        // move straight left
                        if (car.PositionX < 587 && car.PositionX >= 0 && car.PositionY == 94)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                // entry beyond the horizon
                                if (car.VehicleShape.Width < 30)
                                {
                                    Canvas.SetRight(car.VehicleShape, car.PositionX);
                                    Canvas.SetBottom(car.VehicleShape, car.PositionY);
                                    car.VehicleShape.Width += car.Speed;
                                }
                                // move straight
                                else
                                {
                                    Canvas.SetRight(car.VehicleShape, car.PositionX + car.Speed);
                                    Canvas.SetBottom(car.VehicleShape, car.PositionY);
                                    car.PositionX += car.Speed;
                                }
                                // stop if the train is coming
                                if (trainList.Count > 0 && car.PositionX >= 580 && car.PositionY == 94)
                                {
                                    car.Speed = 0;
                                }
                                else
                                {
                                    car.Speed = car.TargetSpeed;
                                }
                                // distance checking / braking
                                CarSpeedAdjustment(car, carsListBottomTemp);
                            });                               
                        }
                        // inside turn to the right
                        else if (car.PositionX >= 587 && car.PositionY >= 94 && Math.Round(car.PositionY) <= 218)
                        {                             
                            this.Dispatcher.Invoke(() =>
                            {
                                (car.PositionX, car.PositionY) = Rotate(587, 94, car.PositionX, car.PositionY, 218, 1 * car.Speed);
                                Canvas.SetRight(car.VehicleShape, car.PositionX);
                                Canvas.SetBottom(car.VehicleShape, car.PositionY);
                                car.Rotate.Angle += car.Speed;
                                car.VehicleShape.RenderTransform = car.Rotate;
                                // distance checking / braking
                                CarSpeedAdjustment(car, carsListBottomTemp);
                            });
                        }
                        // move straight right
                        else if (car.PositionX > 221 && Math.Round(car.PositionY) == 218)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                Canvas.SetRight(car.VehicleShape, car.PositionX - car.Speed);
                                car.PositionX -= car.Speed;
                                car.Rotate.Angle = 180;
                                car.VehicleShape.RenderTransform = car.Rotate;
                                car.ListPosition = carsListBottomTemp.IndexOf(car);
                                // distance checking / braking
                                CarSpeedAdjustment(car, carsListBottomTemp);
                            });
                        }
                        // Turn left on the outside
                        else if (car.PositionX <= 221 && Math.Round(car.PositionY) >= 218 && car.PositionY <= 396)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                (car.PositionX, car.PositionY) = Rotate(221, 218, car.PositionX, car.PositionY, 396, -1 * car.Speed);
                                Canvas.SetRight(car.VehicleShape, car.PositionX);
                                Canvas.SetBottom(car.VehicleShape, car.PositionY);
                                car.Rotate.Angle -= car.Speed;
                                car.VehicleShape.RenderTransform = car.Rotate;
                                // limit speed sign to 60
                                if (car.PositionY >= 300 && car.Speed > 0.25)
                                {
                                    car.Speed = 0.25 * multyply;
                                }
                                // distance checking / braking
                                CarSpeedAdjustment(car, carsListBottomTemp);
                                
                            });
                        }
                        // move straight left
                        else if (car.PositionX <= 800)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                Canvas.SetRight(car.VehicleShape, car.PositionX + car.Speed);
                                car.PositionX += car.Speed;
                                car.Rotate.Angle = 0;
                                car.VehicleShape.RenderTransform = car.Rotate;
                                // distance checking / braking
                                CarSpeedAdjustment(car, carsListBottomTemp);
                            });
                        }
                        // object destruction
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
                        // move straight right
                        if (car.PositionX < 522 && car.PositionX >= 0 && car.PositionY == 175)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                // entry beyond the horizon
                                if (car.VehicleShape.Width < 30)
                                {
                                    Canvas.SetLeft(car.VehicleShape, car.PositionX);
                                    Canvas.SetTop(car.VehicleShape, car.PositionY);
                                    car.VehicleShape.Width += car.Speed;
                                }
                                // move straight right
                                else
                                {
                                    Canvas.SetLeft(car.VehicleShape, car.PositionX + car.Speed);
                                    Canvas.SetTop(car.VehicleShape, car.PositionY);
                                    car.PositionX += car.Speed;
                                }
                                // distance checking / braking
                                CarSpeedAdjustment(car, carsListTopTemp);
                            });
                        }
                        // inside turn to the right
                        else if (car.PositionX >= 522 && car.PositionY >= 175 && Math.Round(car.PositionY) <= 293)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                (car.PositionX, car.PositionY) = Rotate(522, 175, car.PositionX, car.PositionY, 293, 1 * car.Speed);
                                Canvas.SetLeft(car.VehicleShape, car.PositionX);
                                Canvas.SetTop(car.VehicleShape, car.PositionY);
                                car.Rotate.Angle += car.Speed;
                                car.VehicleShape.RenderTransform = car.Rotate;
                                // distance checking / braking
                                CarSpeedAdjustment(car, carsListTopTemp);
                            });
                        }
                        // move straight left
                        else if (car.PositionX > 155 && Math.Round(car.PositionY) == 293)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                Canvas.SetLeft(car.VehicleShape, car.PositionX - car.Speed);
                                car.PositionX -= car.Speed;
                                car.Rotate.Angle = 180;
                                car.VehicleShape.RenderTransform = car.Rotate;
                                // distance checking / braking
                                CarSpeedAdjustment(car, carsListTopTemp);
                            });
                        }
                        // Turn left on the outside
                        else if (car.PositionX <= 155 && Math.Round(car.PositionY) >= 293 && car.PositionY <= 475)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                (car.PositionX, car.PositionY) = Rotate(155, 293, car.PositionX, car.PositionY, 475, -1 * car.Speed);
                                Canvas.SetLeft(car.VehicleShape, car.PositionX);
                                Canvas.SetTop(car.VehicleShape, car.PositionY);
                                car.Rotate.Angle -= car.Speed;
                                car.VehicleShape.RenderTransform = car.Rotate;
                                
                                if (trainList.Count > 0 && car.PositionY >= 345 && car.PositionY <= 355)
                                {
                                    car.Speed = 0;
                                }
                                else
                                {
                                    car.Speed = car.TargetSpeed;
                                }
                                // distance checking / braking
                                CarSpeedAdjustment(car, carsListTopTemp);
                            });
                        }
                        // move straight right
                        else if (car.PositionX <= 800)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                Canvas.SetLeft(car.VehicleShape, car.PositionX + car.Speed);
                                car.PositionX += car.Speed;
                                car.Rotate.Angle = 0;
                                car.VehicleShape.RenderTransform = car.Rotate;
                                // distance checking / braking
                                CarSpeedAdjustment(car, carsListTopTemp);
                            });
                        }
                        // object destruction
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
                    // Servicing railway barrier - opening and closing
                    this.Dispatcher.Invoke(() =>
                    {
                        if(trainList.Count > 0)
                        { 
                            barrierBot.Source = new BitmapImage(new Uri(@"Resources/railway_barrier_2.png", UriKind.Relative));
                            barrierTop.Source = new BitmapImage(new Uri(@"Resources/railway_barrier_3.png", UriKind.Relative));
                        }
                        else
                        {
                            barrierBot.Source = new BitmapImage(new Uri(@"Resources/railway_barrier_1.png", UriKind.Relative));
                            barrierTop.Source = new BitmapImage(new Uri(@"Resources/railway_barrier_4.png", UriKind.Relative));
                        }
                    });
                    List<Vehicle> trainListTemp = new List<Vehicle>(trainList);
                    foreach (Train train in trainListTemp)
                    {
                        if (train.PositionX < 800)
                        {
                            // move straight left
                            if (train.StartingPosition == "right")
                            {
                                this.Dispatcher.Invoke(() =>
                                {
                                    // entry beyond the horizon
                                    if (train.VehicleShape.Width < 150)
                                    {
                                        Canvas.SetRight(train.VehicleShape, train.PositionX);
                                        train.VehicleShape.Width += train.Speed;
                                    }
                                    // move straight left
                                    else
                                    {
                                        Canvas.SetRight(train.VehicleShape, train.PositionX + train.Speed);
                                        train.PositionX += train.Speed;
                                    }
                                });
                            }
                            // move straight right
                            else
                            {
                                // due to the fact that the left train spawn close to the road, it is put to sleep for a moment at the start
                                if (stop)
                                {
                                    Thread.Sleep(3000);
                                    stop = false;
                                }
                                this.Dispatcher.Invoke(() =>
                                {
                                    // entry beyond the horizon

                                    if (train.VehicleShape.Width < 150)
                                    {
                                        Canvas.SetLeft(train.VehicleShape, train.PositionX);
                                        train.VehicleShape.Width += train.Speed;
                                    }
                                    // move straight right
                                    else
                                    {
                                        Canvas.SetLeft(train.VehicleShape, train.PositionX + train.Speed);
                                        train.PositionX += train.Speed;
                                    }
                                });
                            }
                        }
                        // object destruction
                        else
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                CanvasMain.Children.Remove(train.VehicleShape);
                                CanvasMain.InvalidateVisual();
                                trainList.Remove(train);
                                stop = true;
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
        // function for driving a car around a curve
        public (double, double) Rotate(double x,double startY,double actualX,double actualY, double endY,double degree)
        {
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
        // function to calculate the distance of cars
        public double CarDistance(double car1x, double car1y, double car2x, double car2y)
        {
            return Math.Round(Math.Sqrt(Math.Pow(car2x - car1x, 2) + Math.Pow(car2y + -car1y, 2)), 2);
        }
        // function to brake the car when it catches up with the successor 
        public void CarSpeedAdjustment(Car car, List<Vehicle> carsListBottom)
        {
            car.ListPosition = carsListBottom.IndexOf(car);
            if (carsListBottom.Count > 1 && car.ListPosition >= 1)
            {
                if (CarDistance(car.PositionX, car.PositionY, carsListBottom[car.ListPosition - 1].PositionX, carsListBottom[car.ListPosition - 1].PositionY) <= car.SafeDistance)
                {
                    car.Speed = carsListBottom[car.ListPosition - 1].Speed;
                }
            }
        }
        // function to close the application
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {          
            Environment.Exit(Environment.ExitCode);
        }

        protected void IsDebugOrExe()
        {
            if (Debugger.IsAttached)
                multyply = 1;
            else
                multyply = 7;
        }
        #endregion
        #endregion
    }
}
