using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ProjektWatki
{
    public class Car : Vehicle
    {
        #region Fields
        private int listPosition;
        private static int counterTop = 0;
        private static int counterBottom = 0;
        public string startingPosition; // bottom, top
        #endregion
        #region Constructor&Destructor
        public Car(int speed, int priority, string startingPosition) : base(speed, priority)
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
            int choice = random.Next(1, 6);
            switch (choice)
            {
                case 1:
                    randomBrush.Color = Colors.Blue;
                    break;
                case 2:
                    randomBrush.Color = Colors.Red;
                    break;
                case 3:
                    randomBrush.Color = Colors.Pink;
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
            }
        }
        #endregion
    }
}
