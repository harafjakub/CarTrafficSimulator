using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ProjektWatki
{
    public class Car : Vehicle
    {
        #region Fields
        private int listPosition;
        private double safeDistance;
        private double targetSpeed;
        private static int counterTop = 0;
        private static int counterBottom = 0;
        private RotateTransform rotate;
        #endregion
        #region Constructor&Destructor
        public Car(double speed, double positionX, double positionY, double safeDistance, string startingPosition) : base(speed, positionX, positionY, startingPosition)
        {
            this.safeDistance = safeDistance;
            this.targetSpeed = speed;
            this.rotate = new RotateTransform(0, 15, 10);
            if (startingPosition == "top")
            {
                listPosition = counterTop;
                counterTop++;
            }
            else
            {
                listPosition = counterBottom;
                counterBottom++;
            }
            CreateShape();
        }
        ~Car()
        {

        }
        #endregion
        #region Getters&Setters
        public RotateTransform Rotate
        {
            get 
            {
                return rotate; 
            }
            set 
            { 
                rotate = value; 
            }
        }
        public double SafeDistance
        {
            get
            {
                return safeDistance;
            }
            set
            {
                safeDistance = value;
            }
        }
        public double TargetSpeed
        {
            get
            {
                return targetSpeed;
            }
            set
            {
                targetSpeed = value;
            }
        }
        public int ListPosition
        {
            get
            {
                return listPosition;
            }
            set
            {
                listPosition = value;
            }
        }
        #endregion
        #region Methods
        protected override void CreateShape()
        {
            base.CreateShape();
            vehicleShape.Height = 20;
            vehicleShape.Width = 30;
            ImageBrush imgBrush = new ImageBrush();
            RandomColor(imgBrush);
            vehicleShape.Fill = imgBrush;
        }
        protected override void RandomColor(ImageBrush randomImage)
        {
            Random random = new Random();
            int choice = random.Next(1, 13);
            if (startingPosition == "top")
            {
                switch (choice)
                {
                    case 1:
                        randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/car1t.png", UriKind.Relative));
                        break;
                    case 2:
                        randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/car2t.png", UriKind.Relative));
                        break;
                    case 3:
                        randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/car3t.png", UriKind.Relative));
                        break;
                    case 4:
                        randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/car4t.png", UriKind.Relative));
                        break;
                    case 5:
                        randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/car5t.png", UriKind.Relative));
                        break;
                    case 6:
                        randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/car6t.png", UriKind.Relative));
                        break;
                    case 7:
                        randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/car7t.png", UriKind.Relative));
                        break;
                    case 8:
                        randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/car8t.png", UriKind.Relative));
                        break;
                    case 9:
                        randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/car9t.png", UriKind.Relative));
                        break;
                    case 10:
                        randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/car10t.png", UriKind.Relative));
                        break;
                    case 11:
                        randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/car11t.png", UriKind.Relative));
                        break;
                    case 12:
                        randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/car12t.png", UriKind.Relative));
                        break;
                }
            }
            else
            {
                switch (choice)
                {
                    case 1:
                        randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/car1b.png", UriKind.Relative));
                        break;
                    case 2:
                        randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/car2b.png", UriKind.Relative));
                        break;                                                            
                    case 3:                                                               
                        randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/car3b.png", UriKind.Relative));
                        break;                                                            
                    case 4:                                                               
                        randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/car4b.png", UriKind.Relative));
                        break;                                                            
                    case 5:                                                               
                        randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/car5b.png", UriKind.Relative));
                        break;                                                            
                    case 6:                                                               
                        randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/car6b.png", UriKind.Relative));
                        break;                                                            
                    case 7:                                                               
                        randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/car7b.png", UriKind.Relative));
                        break;
                    case 8:
                        randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/car8b.png", UriKind.Relative));
                        break;
                    case 9:
                        randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/car9b.png", UriKind.Relative));
                        break;
                    case 10:
                        randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/car10b.png", UriKind.Relative));
                        break;
                    case 11:
                        randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/car11b.png", UriKind.Relative));
                        break;
                    case 12:
                        randomImage.ImageSource = new BitmapImage(new Uri(@"Resources/car12b.png", UriKind.Relative));
                        break;
                }
            }
        }
        #endregion
    }
}
