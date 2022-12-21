using System.Windows.Media;

namespace ProjektWatki
{
    public class Train: Vehicle
    {
        #region Fields
        private string startingPosition; // left, right
        #endregion
        #region Constructor&Destructor
        public Train(double speed, double positionX, double positionY, string startingPosition) : base(speed, positionX, positionY)
        {
            this.startingPosition = startingPosition;
            CreateShape();
        }
        ~Train() 
        { 
        }
        #endregion
        #region Getters&Setters
        public string StartingPosition
        {
            get
            {
                return startingPosition;
            }
            set
            {
                startingPosition = value;
            }
        }
        #endregion
        #region Methods
        public override void CreateShape()
        {
            base.CreateShape();
            vehicleShape.Height = 30;
            vehicleShape.Width = 150;
            SolidColorBrush brownBrush = new SolidColorBrush();
            brownBrush.Color = Colors.Brown;
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;
            vehicleShape.StrokeThickness = 4;
            vehicleShape.Stroke = blackBrush;
            vehicleShape.Fill = brownBrush;
            vehicleShape.RadiusX = 10;
            vehicleShape.RadiusY = 10;
        }
        #endregion
    }
}
