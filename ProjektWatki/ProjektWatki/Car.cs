using System;
using System.Windows.Media;

namespace ProjektWatki
{
    public class Car : Vehicle
    {
        //com1
        #region Fields
        private int listPosition;
        private static int counterTop = 0;
        private static int counterBottom = 0;
        public RotateTransform rotate = new RotateTransform(0,15,10);
        private string startingPosition; // bottom, top
        #endregion
        #region Constructor&Destructor
        public Car(double speed, double positionX, double positionY, string startingPosition) : base(speed, positionX, positionY)
        {
            CreateShape();   
            this.startingPosition = startingPosition;
            if(startingPosition == "top")
            {
                listPosition = counterTop;
                counterTop++;
            }
            else
            {
                listPosition = counterBottom;
                counterBottom++;
            }
        }
        ~Car()
        {

        }
        #endregion
        #region Getters&Setters
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
        public override void CreateShape()
        {
            base.CreateShape();
            vehicleShape.Height = 20;
            vehicleShape.Width = 30;
            SolidColorBrush randomBrush = new SolidColorBrush();
            RandomColor(randomBrush);
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;
            vehicleShape.StrokeThickness = 4;
            vehicleShape.Stroke = blackBrush;
            vehicleShape.Fill = randomBrush;
            vehicleShape.RadiusX = 10;
            vehicleShape.RadiusY = 10;
        }
        private void RandomColor(SolidColorBrush randomBrush)
        {
            Random random = new Random();
            int choice = random.Next(1, 16);
            switch (choice)
            {
                case 1:
                    randomBrush.Color = Colors.Blue;
                    break;
                case 2:
                    randomBrush.Color = Colors.Red;
                    break;
                case 3:
                    randomBrush.Color = Colors.Black;
                    break;
                case 4:
                    randomBrush.Color = Colors.Yellow;
                    break;
                case 5:
                    randomBrush.Color = Colors.Green;
                    break;
                case 6:
                    randomBrush.Color = Colors.Orange;
                    break;
                case 7:
                    randomBrush.Color = Colors.Gray;
                    break;
                case 8:
                    randomBrush.Color = Colors.White;
                    break;
                case 9:
                    randomBrush.Color = Colors.Silver;
                    break;
                case 10:
                    randomBrush.Color = Colors.DeepPink;
                    break;
                case 11:
                    randomBrush.Color = Colors.DeepSkyBlue;
                    break;
                case 12:
                    randomBrush.Color = Colors.Magenta;
                    break;
                case 13:
                    randomBrush.Color = Colors.DimGray;
                    break;
                case 14:
                    randomBrush.Color = Colors.LightGray;
                    break;
                case 15:
                    randomBrush.Color = Colors.DarkOliveGreen;
                    break;
            }
        }
        #endregion
    }
}
