using System.Windows.Media;
using System.Windows.Shapes;

namespace ProjektWatki
{
    public abstract class Vehicle
    {
        #region Fields
        protected double speed;
        protected double positionX;
        protected double positionY;
        protected string startingPosition; // (Train - left, right) (Car - top, bottom)
        protected Rectangle vehicleShape;
        #endregion
        #region Coonstructor&Destructor
        public Vehicle(double speed, double positionX, double positionY, string startingPosition)
        {
            this.speed = speed;
            this.positionX = positionX;
            this.positionY = positionY;
            this.startingPosition = startingPosition;
            this.vehicleShape = new Rectangle();
        }
        ~Vehicle() 
        { 
        }
        #endregion
        #region Getters&Setters
        public Rectangle VehicleShape
        {
            get
            {
                return vehicleShape;
            }
            set
            {
                vehicleShape = value;
            }
        }
        public double Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }
        public double PositionX
        {
            get
            {
                return positionX;
            }
            set
            {
                positionX = value;
            }
        }
        public double PositionY
        {
            get
            {
                return positionY;
            }
            set
            {
                positionY = value;
            }
        }
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
        protected virtual void CreateShape()
        {

        }
        protected virtual void RandomColor(ImageBrush randomImage)
        {

        }
        #endregion
    }
}
